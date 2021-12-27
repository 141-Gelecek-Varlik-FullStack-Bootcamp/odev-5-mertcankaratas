using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,string recordId, T data , TimeSpan? absoluteExpireTime = null,TimeSpan? unusedExpireTime =null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(30);
            var jsonData = JsonSerializer.Serialize(data);

            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task SetListRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(30);
            var jsonData = JsonSerializer.Serialize(data);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonData);
            await cache.SetAsync(recordId,bytes, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId); 
            if(jsonData == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);

            
        }


        public static async Task<T> GetListRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetAsync(recordId);
            if (jsonData == null)
            {
                return default(T);
            }
            var bytes = Encoding.ASCII.GetString(jsonData);
            return JsonSerializer.Deserialize<T>(bytes);


        }


        public static async Task DeleteRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData == null)
            {
                return;
            }
           
            await cache.RemoveAsync(recordId);


        }
    }
}
