using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using System.Net;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly StepsContext _context;

        public CommonSteps(StepsContext context)
        {
            _context = context;
        }

        [Then(@"Tom should receive status codes (.*)")]
        [Then(@"Amy should receive status codes (.*)")]
        public void ThenTomShouldReceiveSuccessResult(HttpStatusCode code)
        {
            (_context.ApiResult.StatusCode == code).Should().BeTrue();
        }
    }
}
