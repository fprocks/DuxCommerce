using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202011181900)]
    public class CreateStoreProfileTable : Migration
    {
        public override void Up()
        {
            Create.Table("StoreProfile")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("StoreName").AsString(255).NotNullable()
                .WithColumn("ContactEmail").AsString(100).NotNullable()
                .WithColumn("SenderEmail").AsString(100).NotNullable()
                .WithColumn("BusinessName").AsString(255).Nullable()
                .WithColumn("PhoneNumber").AsString(50).NotNullable()
                .WithColumn("TimeZoneId").AsString(50).NotNullable()
                .WithColumn("UnitSystem").AsString(20).NotNullable()
                .WithColumn("WeightUnit").AsString(20).NotNullable()
                .WithColumn("LengthUnit").AsString(20).NotNullable()
                .WithColumn("AddressId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("StoreProfile");
        }
    }
}