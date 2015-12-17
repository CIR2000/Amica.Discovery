using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Amica.vNext
{
    // TODO GetServicesAddresses which returns the list of available Uris for a given service.

    public class Discovery : IDiscovery
    {
        private const string DiscoveryName = "Discovery";
        private readonly SqliteObjectCache _cache;

        public Discovery()
        {
			// TODO maybe make ApplicationName a required constructor field because we could
			// end up not knowing where the cache is actually stored (callerName happen to be null).
            var callerName = Assembly.GetEntryAssembly()?.FullName.Split(',')[0];
            var applicationName = System.IO.Path.Combine(
				callerName ?? "DisoveryDefaultApplication", DiscoveryName);

            _cache = new SqliteObjectCache() { ApplicationName = applicationName };
        }
        public async Task<ApiService> GetService(ApiKind kind, Version version = null, bool ignoreCache=false)
        {

			var cacheKey = (version == null) ? $"{kind}-service" : $"{kind}-v{version}-service";
            if (ignoreCache == false)
            {
                try
                {
                    return await _cache.Get<ApiService>(cacheKey);
                }
                catch (KeyNotFoundException) { }
            }

            var query = new StringBuilder();
            query.Append("{");
			query.Append($"\"kind\": \"{kind}\"");
            if (version != null)
            {
				query.Append($", \"services.version.major\": {version.Major}");
				query.Append($", \"services.version.minor\": {{\"$gte\": {version.Minor}}}");
				//query.Append($", \"services.version.build\": {version.Build}");
            }
            query.Append("}");

            var apis = await PerformRequest(query.ToString());
            if (apis.Count == 0)
                throw new ApiNotAvailableDiscoveryException();

			// by default documents are sorted by version, descending.
            var service = apis[0].Services[0];

			await _cache.Insert(cacheKey, service);

            return service;
        }

        public async Task<Uri> GetServiceAddress(ApiKind kind, Version version = null, bool ignoreCache=false)
        {
            var service = await GetService(kind, version, ignoreCache);
            return service?.BaseAddress;
        }

        public async Task<Api> GetApi(ApiKind kind, bool ignoreCache=false)
        {
            var cacheKey = $"{kind}-api";
            if (ignoreCache == false)
            {
                try
                {
                    return  await _cache.Get<Api>(cacheKey);
                }
                catch (KeyNotFoundException) { }
            }

            var query = new StringBuilder();
	    query.Append($"{{\"kind\": \"{kind}\"}}");
            var apis = await PerformRequest(query.ToString());

            if (apis.Count == 0)
                throw new ApiNotAvailableDiscoveryException();

	    // By design only one API of a given kind can exist.
            var api = apis[0];

	    await _cache.Insert(cacheKey, api);

            return api;
        }

        private async Task<List<Api>> PerformRequest(string query)
        {
            if (BaseAddress == null)
                throw new ArgumentNullException(nameof(BaseAddress));

            var eve = new Eve.EveClient() { BaseAddress = BaseAddress };

            return await eve.GetAsync<Api>("apis", false, query);
        }
	public  Uri BaseAddress { get; set; }
    }
}
