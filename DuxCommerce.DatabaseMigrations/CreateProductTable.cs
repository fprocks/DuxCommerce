using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(20200926102800)]
    public class CreateProductTable : Migration
    {
        public override void Up()
        {
            Create.Table("ProductModel")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Retail").AsDecimal().NotNullable()
                .WithColumn("Cost").AsDecimal().NotNullable()
                .WithColumn("Length").AsDecimal().NotNullable()
                .WithColumn("Width").AsDecimal().NotNullable()
                .WithColumn("Height").AsDecimal().NotNullable()
                .WithColumn("Weight").AsDecimal().NotNullable()
                .WithColumn("ShippingType").AsString(50).NotNullable()
                .WithColumn("SKU").AsString(255).NotNullable()
                .WithColumn("Barcode").AsString(255).NotNullable()
                .WithColumn("TrackInventory").AsBoolean().NotNullable()
                .WithColumn("OutOfStockRule").AsString(50).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ProductModel");
        }
    }
}