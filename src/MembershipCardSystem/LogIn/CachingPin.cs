//using System;
//using System.Threading.Tasks;
//using MembershipCardSystem.LogIn.Model;
//using Microsoft.Extensions.Caching.Memory;
//
//namespace MembershipCardSystem.LogIn
//{
//    public class CachingPin
//    {
//        private readonly IMemoryCache _memorycache;
//
//        public CachingPin(IMemoryCache memoryCache)
//        {
//            _memorycache = memoryCache;
//
//        }
//
//        public async Task<Pin> IssueCachedPin(string cardId)
//        {
//            var cacheKey = cardId;
//
//            if (_memorycache.TryGetValue(cacheKey, out Pin cachedPin))
//            {
//                return cachedPin;
//            }
//
//            return null;
//        }
//
//        public bool IsTokenCached(string cardId)
//        {
//            var cacheKey = cardId;
//            return _memorycache.TryGetValue(cacheKey, out Pin cachedPin);
//        }
//
//
//        public void CachePin(string freshPin, string cacheKey)
//        {
//            _memorycache.Set(cacheKey, freshPin, TimeSpan.FromMinutes(10));
//        }
//
//    }
//}
