using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace MembershipCardSystem.IntegrationTests
{
    public class TestBase: IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;
        protected readonly IConfigurationRoot Config;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.development.json")
                .Build();
        }
    }
}