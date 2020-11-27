using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202010252300)]
    public class CreateShoppingCartItemTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShoppingCartItem")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CartId").AsInt64().NotNullable()
                .WithColumn("ProductId").AsInt64().NotNullable()
                .WithColumn("ProductName").AsString(255).NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Quantity").AsDecimal().NotNullable()
                .WithColumn("ItemTotal").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShoppingCartItem");
        }
    }
}