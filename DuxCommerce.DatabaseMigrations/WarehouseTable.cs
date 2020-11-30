using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202011290921)]
    public class WarehouseTable : Migration
    {
        public override void Up()
        {
            Create.Table("Warehouse")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("StoreId").AsInt64().NotNullable()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("AddressId").AsInt64().Nullable()
                .WithColumn("IsDefault").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Warehouse");
        }
    }
}