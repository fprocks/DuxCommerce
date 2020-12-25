using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012250015)]
    public class CreateShippingRateItemTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingRateItem")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingRateId").AsInt64().NotNullable()
                .WithColumn("Rate").AsDecimal().NotNullable()
                .WithColumn("Min").AsDecimal().NotNullable()
                .WithColumn("Max").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingRateItem");
        }
    }
}
