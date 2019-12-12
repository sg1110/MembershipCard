using MembershipCardSystem.LogIn.Model;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace MembershipCardSystem.UnitTests.PinMemoryCache
{
    public class FakeMemoryCache : IMemoryCache
    {
        private Pin _cahedPin;

        public FakeMemoryCache(Pin cachedPin)
        {
            _cahedPin = cachedPin;

        }
        
        public void Dispose()
        {
        }

        public bool TryGetValue(object key, out object value)
        {
            value = _cahedPin;
            return true;
        }

        public ICacheEntry CreateEntry(object key)
        {
            return new Mock<ICacheEntry>().Object;
        }

        public void Remove(object key)
        {
        }
    }
}