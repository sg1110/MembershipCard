using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MembershipCardSystem.LogIn;
using MembershipCardSystem.LogIn.Model;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace MembershipCardSystem.UnitTests.PinMemoryCache
{
    public class WhenPinIsCached
    {
        private readonly IFixture _fixture;
        private const string cardId = "1234";
        private const string cardPin = "0123456789012345";

        public WhenPinIsCached()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization {ConfigureMembers = true});
        }

        [Fact]
        public async void It_will_check_for_cached_pin()
        {
            var memoryCache = _fixture.Freeze<Mock<IMemoryCache>>();
            var cachingPin = _fixture.Create<CachingPin>();
            
            await cachingPin.IssueCachedPin(cardId);
            
            memoryCache.Verify(m => m.TryGetValue(cardId, out It.Ref<object>.IsAny));

        }

        [Fact]
        public async void It_will_return_cached_pin()
        {
            var cachedPin = _fixture.Create<Pin>();
            _fixture.Register<IMemoryCache>( () => new FakeMemoryCache(cachedPin));
            var cachingPin = _fixture.Create<CachingPin>();

            var resultsPin = await cachingPin.IssueCachedPin(cardId);

            resultsPin.Should().Be(cachedPin);
        }
    }
}