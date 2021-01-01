using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061758)]
    public class CreateShippingMethodTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingMethod")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingZoneId").AsInt64().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("MethodType").AsString(50).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingMethod");
        }
    }
}
