using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012250015)]
    public class CreateShippingRateTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingRate")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingMethodId").AsInt64().NotNullable()
                .WithColumn("Min").AsDecimal().NotNullable()
                .WithColumn("Max").AsDecimal().NotNullable()
                .WithColumn("Rate").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingRate");
        }
    }
}
