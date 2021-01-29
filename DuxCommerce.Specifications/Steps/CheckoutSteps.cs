using System;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class CreateShippingAddressSteps
    {
        [Given(@"Amy enters the email address (.*)")]
        public void GivenAmyEntersTheEmailAddress(string address)
        {
        }
        
        [Given(@"Amy enters the following shipping address")]
        public void GivenAmyEntersTheFollowingShippingAddress(Table table)
        {
        }
        
        [When(@"Amy saves the shipping address")]
        public void WhenAmySavesTheShippingAddress()
        {
        }
        
        [Then(@"the shipping address should be created as expected")]
        public void ThenTheShippingAddressShouldBeCreatedAsExpected()
        {
        }
    }
}
