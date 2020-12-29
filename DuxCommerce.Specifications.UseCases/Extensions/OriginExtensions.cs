using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuxCommerce.Specifications.UseCases.Extensions
{
    public static class OriginExtensions
    {
        public static bool EqualTo(this List<ShippingOrigin> expectedOrigins, List<ShippingOriginDto> actualOrigins)
        {
            for (var index = 0; index < expectedOrigins.Count; index++)
            {
                if (!expectedOrigins[index].EqualTo(actualOrigins[index]))
                    return false;
            }

            return true;
        }

        private static bool EqualTo(this ShippingOrigin expected, ShippingOriginDto actual)
        {
            return expected.Name == actual.Name &&
                expected.IsDefault == actual.IsDefault &&
                expected.AddressLine1 == actual.Address.AddressLine1 &&
                expected.AddressLine2 == actual.Address.AddressLine2 &&
                expected.City == actual.Address.City &&
                expected.PostalCode == actual.Address.PostalCode &&
                expected.StateName == actual.Address.StateName &&
                expected.CountryCode == actual.Address.CountryCode;
        }
    }
}
