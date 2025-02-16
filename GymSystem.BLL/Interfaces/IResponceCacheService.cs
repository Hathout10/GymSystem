using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Interfaces
{
    public interface IResponceCacheService
    {
        Task CacheResponceAsync(string cacheKey, object responce, TimeSpan timeToLive);

        Task<string> GetCachedResponce(string cacheKey);
    }
}
