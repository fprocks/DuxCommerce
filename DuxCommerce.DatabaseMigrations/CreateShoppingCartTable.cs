using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(20201025225400)]
    public class CreateShoppingCartTable : Migration
    {
        public override void Up()
        {
            Create.Table("ShoppingCart")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ShopperId").AsInt64().NotNullable()
                .WithColumn("CartTotal").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShoppingCart");
        }
    }
}