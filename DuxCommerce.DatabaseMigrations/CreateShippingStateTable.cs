using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012062315)]
    public class CreateShippingStateTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingState")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShippingCountryId").AsInt64().NotNullable()
                .WithColumn("StateId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingState");
        }
    }
}
