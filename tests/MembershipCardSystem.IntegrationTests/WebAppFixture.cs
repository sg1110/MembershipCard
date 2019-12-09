using System;
using Alba;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MembershipCardSystem.IntegrationTests
{
    public class WebAppFixture : IDisposable
    {
        public SystemUnderTest SystemUnderTest { get; } // = SystemUnderTest.ForStartup<Startup>();

        public WebAppFixture()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("development");
            SystemUnderTest = new SystemUnderTest(builder, typeof(Startup).Assembly);
        }

        public void Dispose()
        {
            SystemUnderTest?.Dispose();
        }
    }
}