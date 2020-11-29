using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly Hooks.ScenarioContext _context;

        public CommonSteps(Hooks.ScenarioContext context)
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
