using System;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class WarehouseSteps
    {
        [Then(@"the store warehouse should be created as follow:")]
        public void ThenTheStoreWarehouseShouldBeCreatedAsFollow(Table table)
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
