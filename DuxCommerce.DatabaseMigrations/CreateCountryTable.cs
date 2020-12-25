using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012121140)]
    public class CreateCountryTable : Migration
    {
        public override void Up()
        {
            Create.Table("Country")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("ISOCode").AsString(2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Country");
        }
    }
}
