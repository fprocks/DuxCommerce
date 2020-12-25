using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012121150)]
    public class CreateStateTable : Migration
    {
        public override void Up()
        {
            Create.Table("State")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CountryCode").AsString(2).NotNullable()
                .WithColumn("Name").AsString(100).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("State");
        }
    }
}
