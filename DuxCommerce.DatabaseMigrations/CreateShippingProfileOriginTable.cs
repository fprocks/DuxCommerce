using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061015)]
    public class CreateShippingProfileOriginTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingProfileOrigin")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingProfileId").AsInt64().NotNullable()
                .WithColumn("ShippingOriginId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingProfileOrigin");
        }
    }
}
