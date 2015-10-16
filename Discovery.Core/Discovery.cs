using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Amica.vNext
{
    public class Discovery : IDiscovery
    {
        private const string DiscoveryName = "Discovery";
        private SqliteObjectCache _cache;

        public Discovery()
        {
            var callerName = Assembly.GetEntryAssembly().FullName.Split(',')[0];
            var applicationName = System.IO.Path.Combine(callerName, DiscoveryName);
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

        public async Task<Uri> GetServiceUri(ApiKind kind, Version version = null, bool ignoreCache=false)
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

        private static async Task<List<Api>> PerformRequest(string query)
        {
            var eve = new Eve.EveClient() { BaseAddress = new Uri("http://10.0.2.2:9000") };

            return await eve.GetAsync<Api>("apis", false, query);
        }
    }
}
