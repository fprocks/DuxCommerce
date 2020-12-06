using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061012)]
    public class CreateShippingZoneTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingZone")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("ShippingProfileId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingZone");
        }
    }
}
