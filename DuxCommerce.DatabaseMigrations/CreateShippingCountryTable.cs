using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012062311)]
    public class CreateShippingCountryTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingCountry")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingZoneId").AsInt64().NotNullable()
                .WithColumn("CountryCode").AsString(2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingCountry");
        }
    }
}
