using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061758)]
    public class CreateShippingRateTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingRate")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingZoneId").AsInt64().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("RateType").AsString(50).NotNullable()
                .WithColumn("Rate").AsDecimal().NotNullable()
                .WithColumn("Min").AsDecimal().NotNullable()
                .WithColumn("Max").AsDecimal().Nullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingRate");
        }
    }
}
