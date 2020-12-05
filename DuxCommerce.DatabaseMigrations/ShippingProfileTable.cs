using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012061001)]
    public class CreateShippingProfile : Migration
    {
        public override void Up()
        {
            Create.Table("ShippingProfile")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("IsDefault").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ShippingProfile");
        }
    }
}
