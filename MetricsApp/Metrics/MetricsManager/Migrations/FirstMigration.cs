using FluentMigrator;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Agents").WithColumn("AgentId").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentAddress").AsString()
                .WithColumn("Enable").AsBoolean();
        }
        public override void Down()
        {
            Delete.Table("Agents");
        }
    }
}
