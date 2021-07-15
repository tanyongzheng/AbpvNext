using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCaching.Core;
using Volo.Abp;
using Volo.Abp.Validation;

namespace AbpvNext
{
    public abstract class DomainHybridCacheManager<TCacheItem> : DomainManagerBase where TCacheItem : class
    {

        /// <summary>
        /// 混合缓存名
        /// 继承该类的必须设置
        /// </summary>
        public string HybridCacheName { get; set; }

        protected IHybridCachingProvider HybridCachingProvider => LazyServiceProvider.LazyGetRequiredService<IHybridCachingProvider>();


        /// <summary>
        /// 获取缓存项集合
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<TCacheItem>> GetListFromCacheAsync();

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        public async Task RemoveCacheAsync()
        {
            if (string.IsNullOrEmpty(HybridCacheName))
            {
                throw new AbpValidationException("混合缓存名HybridCacheName为空，无法移除缓存");
            }
            await HybridCachingProvider.RemoveAsync(HybridCacheName);
        }

        /// <summary>
        /// 缓存过期时间
        /// 默认30天
        /// </summary>
        public TimeSpan CacheExpiration { get; set; } = TimeSpan.FromDays(30);

        /// <summary>
        /// 缓存过期时间+随机时间
        /// </summary>
        public TimeSpan CacheExpirationAddRandomTime => CacheExpiration + TimeSpan.FromHours(RandomHelper.GetRandom(-5, 5));
    }
}