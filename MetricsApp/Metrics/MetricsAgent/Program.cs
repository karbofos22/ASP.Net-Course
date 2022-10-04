using AutoMapper;
using Dapper;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using MetricsAgent.Jobs;
using MetricsAgent.Mappings;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Implementations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Data.SQLite;
using System.Diagnostics;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Mapping

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            #endregion

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

            #region Configure Repos
            builder.Services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            builder.Services.AddSingleton<IDotnetMetricsRepository, DotnetMetricsRepository>();
            builder.Services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            builder.Services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();

            #endregion

            #region Configure Services

            //Fluent migration framework 
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSQLite()
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                .WithGlobalConnectionString(builder.Configuration.GetSection("Settings:DatabaseOptions:ConnectionString").Value));

            //Quartz framework
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();

            builder.Services.AddSingleton<CpuMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(CpuMetricJob), "0/5 * * ? * * *"));

            builder.Services.AddSingleton<HddMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(HddMetricJob), "0/5 * * ? * * *"));

            builder.Services.AddSingleton<RamMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(RamMetricJob), "0/5 * * ? * * *"));

            //builder.Services.AddSingleton<DontnetMetricJob>();
            //builder.Services.AddSingleton(new JobSchedule(typeof(DontnetMetricJob), "0/5 * * ? * * *"));

            builder.Services.AddSingleton<NetworkMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(NetworkMetricJob), "0/15 * * ? * * *"));

            builder.Services.AddHostedService<QuartzHostedService>();
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

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