using System;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShippingOriginSteps
    {
        [Then(@"default shipping origin should be created as follow:")]
        public void ThenDefaultShippingOriginShouldBeCreatedAsFollow(Table table)
        {
        }
    }
}
