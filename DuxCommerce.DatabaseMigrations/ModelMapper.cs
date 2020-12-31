using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.Settings.Dto;
using DuxCommerce.ShoppingCarts.PublicTypes;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<ProductDto>()
                .Table("[dbo].[Product]");
            
            FluentMapper.Entity<ShoppingCartDto>()
                .Table("[dbo].[ShoppingCart]");
            
            FluentMapper.Entity<CartItemDto>()
                .Table("[dbo].[ShoppingCartItem]");

            FluentMapper.Entity<CountryDto>()
                .Table("[dbo].[Country]");

            FluentMapper.Entity<StateDto>()
                .Table("[dbo].[State]");

            FluentMapper.Entity<StoreProfileDto>()
                .Table("[dbo].[StoreProfile]");

            FluentMapper.Entity<AddressDto>()
                .Table("[dbo].[Address]");

            FluentMapper.Entity<ShippingOriginDto>()
                .Table("[dbo].[ShippingOrigin]");

            FluentMapper.Entity<ShippingProfileDto>()
                .Table("[dbo].[ShippingProfile]");

            FluentMapper.Entity<ShippingProfileOriginDto>()
                .Table("[dbo].[ShippingProfileOrigin]");
            
            FluentMapper.Entity<ShippingZoneDto>()
                .Table("[dbo].[ShippingZone]");

            FluentMapper.Entity<ShippingCountryDto>()
                .Table("[dbo].[ShippingCountry]");

            FluentMapper.Entity<ShippingStateDto>()
                .Table("[dbo].[ShippingState]");
            
            FluentMapper.Entity<ShippingRateDto>()
                .Table("[dbo].[ShippingRate]");

            FluentMapper.Entity<ShippingRateItemDto>()
                .Table("[dbo].[ShippingRateItem]");
        }
    }
}