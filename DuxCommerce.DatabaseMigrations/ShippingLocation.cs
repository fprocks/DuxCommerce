using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061015)]
    public class CreateShippingLocationTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingLocation")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingProfileId").AsInt64().NotNullable()
                .WithColumn("LocationId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingLocation");
        }
    }
}
