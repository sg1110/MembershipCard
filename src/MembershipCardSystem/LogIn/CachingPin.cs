using System;
using System.Threading;
using System.Threading.Tasks;
using MembershipCardSystem.LogIn.Model;
using Microsoft.Extensions.Caching.Memory;

namespace MembershipCardSystem.LogIn
{
    public class CachingPin
    {
        private readonly IMemoryCache _memorycache;
        private SemaphoreSlim _semaphore;

        public CachingPin(IMemoryCache memoryCache)
        {
            _memorycache = memoryCache;
            _semaphore = new SemaphoreSlim(1);

        }

//        public async Task<Pin> IssueCachedPin(string cardId)
//        {
//            var cacheKey = cardId;
//            
//            if (_memorycache.TryGetValue(cacheKey, out Pin cachedPin))
//            {
//                return cachedPin;
//            }
//
//            await _semaphore.WaitAsync();
//            try
//            {
//                if (_memorycache.TryGetValue(cacheKey, out cachedPin))
//                {
//                    return cachedPin;
//                }
//
//                var pin = cachedPin.CachedPin.Contains(cardId);
//            }
//            finally
//            {
//                _semaphore.Release();
//            }
//
//            return cachedPin;
//        }

        public string IssueCachedPin(string cardId)
        {
            (_memorycache.TryGetValue(cardId, out string pin)).ToString();
            return pin;
        }

        public bool IsPinCached(string cardId)
        {
            var result = IssueCachedPin(cardId);
            return !string.IsNullOrEmpty(result);
        }


        public void CachePin(string freshPin, string cacheKey)
        {
            var timeNow = DateTime.Now;
            var expirationTime = timeNow.AddMinutes(5);
                _memorycache.Set(cacheKey, freshPin, expirationTime);
        }

    }
}
