using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using MembershipCardSystem.LogIn;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace MembershipCardSystem.UnitTests.PinMemoryCache
{
    public class WhenPinIsCached
    {
        private readonly IFixture _fixture;

        public WhenPinIsCached()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization {ConfigureMembers = true});
        }

        [Fact]
        public async void It_will_check_for_cached_pin()
        {
            var memoryCache = _fixture.Freeze<Mock<IMemoryCache>>();
            var cachingPin = _fixture.Create<CachingPin>();

            var cardPin = "1234";
            var cardId = "0123456789012345";

            //   await post to log in page
         memoryCache.Verify(m => m.TryGetValue(cardId, out It.Ref<object>.IsAny));

        }

        [Fact]
        public async void It_will_return_cached_pin()
        {
            throw new NotImplementedException();
            
        }
    }
}