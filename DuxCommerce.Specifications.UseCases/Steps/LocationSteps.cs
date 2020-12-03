using System;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class LocationSteps
    {
        [Then(@"the location should be created as follow:")]
        public void ThenTheLocationShouldBeCreatedAsFollow(Table table)
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
