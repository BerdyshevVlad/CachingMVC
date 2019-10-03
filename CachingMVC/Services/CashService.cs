using CachingMVC.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingMVC.Services
{
    public class CashService
    {
        private IMemoryCache _cache;

        public CashService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResult> TryExecute<TResult>(Func<Task<TResult>> action,int? id)
        {
            try
            {
                TResult product;
                string res = string.Empty;
                if (id.HasValue)
                {
                    res = id.Value.ToString() + typeof(TResult).FullName;
                }

                if (!_cache.TryGetValue(res, out product))
                {
                    TResult result = await action();

                    _cache.Set((result as BaseEntity).Id.ToString() + result.GetType().ToString(), result, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });

                    return result;
                }

                return product;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
