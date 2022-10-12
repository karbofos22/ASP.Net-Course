using AutoMapper;
using Dapper;
using FluentMigrator.Runner;
using MetricsManager.Models;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using MetricsManager.Services.Client.Implementations;
using MetricsManager.Services.Implementations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Polly;

namespace MetricsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Options

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
            });

            #endregion

            #region Configure Logging

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            #endregion

            #region Services

            //Fluent migration framework 
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSQLite()
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                .WithGlobalConnectionString(builder.Configuration.GetSection("Settings:DatabaseOptions:ConnectionString").Value));

            #endregion

            #region Configure Repos
            builder.Services.AddSingleton<IAgentsRepository, AgentsRepository>();


            #endregion

            #region Configure HttpClient
            builder.Services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3, 
                sleepDurationProvider: (attempCount) => TimeSpan.FromSeconds(attempCount * 2),
                onRetry: (exeption, sleepDuration, attempCount, context) =>
                {
                    var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                    logger.LogError(exeption.Exception != null ? exeption.Exception :
                        new Exception($"\n{exeption.Result.StatusCode}: {exeption.Result.RequestMessage}"),
                        $"attemp: {attempCount} request exeption");
                }));

            #endregion

            //Cast string to Uri
            SqlMapper.AddTypeHandler(new UriTypeHandler());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

                // Поддержка TimeSpan
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            using var serviceScoped = app.Services.CreateScope();
            var services = serviceScoped.ServiceProvider;
            var migrationRunner = services.GetRequiredService<IMigrationRunner>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            migrationRunner.MigrateUp();

            app.Run();
        }
    }
}