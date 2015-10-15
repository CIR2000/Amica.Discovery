using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amica.vNext
{
    public class Discovery : IDiscovery
    {
        public async Task<ApiService> GetService(ApiKind kind, Version version = null)
        {

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

	    // TODO what to return if no matches? Exception or null?
	    // by default documents are sorted by version, descending.
            return (apis.Count > 0) ? apis[0].Services[0] : null;
        }

        public async Task<Uri> GetServiceUri(ApiKind kind, Version version = null)
        {
            var service = await GetService(kind, version);
            return service?.BaseAddress;
        }

        public async Task<List<Api>> GetApi(ApiKind kind)
        {
            var query = new StringBuilder();
	    query.Append($"{{\"kind\": \"{kind}\"}}");
            return await PerformRequest(query.ToString());
        }

        private static async Task<List<Api>> PerformRequest(string query)
        {
            var eve = new Eve.EveClient() { BaseAddress = new Uri("http://10.0.2.2:9000") };

            return await eve.GetAsync<Api>("apis", false, query);
        }
    }
}
