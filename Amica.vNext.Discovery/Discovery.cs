using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amica.vNext.Discovery
{
    class Discovery : IDiscovery
    {
        public ApiService GetApiService(ApiKind kind, Version version = null)
        {
            throw new NotImplementedException();
        }

        public Uri GetApiServiceAddress(ApiKind kind, Version version = null)
        {
            throw new NotImplementedException();
        }

        public Api GetApi(ApiKind kind)
        {
            throw new NotImplementedException();
        }
    }
}
