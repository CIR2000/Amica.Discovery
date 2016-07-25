using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleObjectCache;

namespace Amica.vNext
{
    // TODO GetServicesAddresses which returns the list of available Uris for a given service.

    public class Discovery : IDiscovery
    {
        public async Task<ApiService> GetService(ApiKind kind, Version version = null, bool ignoreCache=false)
        {

			var cacheKey = (version == null) ? $"{kind}-service" : $"{kind}-v{version}-service";
            if (ignoreCache == false && LocalCache != null)
            {
                try
                {
                    return await LocalCache.Get<ApiService>(cacheKey).ConfigureAwait(false);
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

            var apis = await PerformRequest(query.ToString()).ConfigureAwait(false);
            if (apis.Count == 0)
                throw new ApiNotAvailableDiscoveryException();

			// by default documents are sorted by version, descending.
            var service = apis[0].Services[0];

			if (LocalCache != null)
				await LocalCache.Insert(cacheKey, service).ConfigureAwait(false);

            return service;
        }

        public async Task<Uri> GetServiceAddress(ApiKind kind, Version version = null, bool ignoreCache=false)
        {
            var service = await GetService(kind, version, ignoreCache).ConfigureAwait(false);
            return service?.BaseAddress;
        }

        public async Task<Api> GetApi(ApiKind kind, bool ignoreCache=false)
        {
            var cacheKey = $"{kind}-api";
            if (ignoreCache == false && LocalCache != null)
            {
                try
                {
                    return  await LocalCache.Get<Api>(cacheKey);
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

			if (LocalCache != null)
				await LocalCache.Insert(cacheKey, api);

            return api;
        }

        private async Task<List<Api>> PerformRequest(string query)
        {
            if (BaseAddress == null)
                throw new ArgumentNullException(nameof(BaseAddress));

            var eve = new Eve.EveClient() { BaseAddress = BaseAddress };

            return await eve.GetAsync<Api>("apis", false, query).ConfigureAwait(false);
        }
	public  Uri BaseAddress { get; set; }
	public IBulkObjectCache LocalCache { get; set; }
    }
}
