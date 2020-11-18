using FluentMigrator;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202011210500)]
    public abstract class CreateAddressTable : Migration
    {
        public override void Up()
        {
            Create.Table("Address")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Address1").AsString(255).NotNullable()
                .WithColumn("Address2").AsString(255).Nullable()
                .WithColumn("Address3").AsString(255).Nullable()
                .WithColumn("City").AsString(100).NotNullable()
                .WithColumn("PostalCode").AsString(50).Nullable()
                .WithColumn("State").AsString(100).Nullable()
                .WithColumn("Country").AsString(2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Address");
        }
    }
}