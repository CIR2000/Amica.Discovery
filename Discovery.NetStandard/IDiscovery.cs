﻿using System;
using System.Threading.Tasks;

namespace Amica.Discovery
{
    interface IDiscovery
    {
        /// <summary>
        /// Returns an API Service of a given kind.
        /// </summary>
        /// <param name="kind">Kind of API.</param>
        /// <param name="version">Version. If null, the most recent version available will be returned.</param>
        /// <param name="ignoreCache">Wether the address stored in the local cache should be ignored.</param>
        /// <returns>An ApiService istance or null if none matched the search criteria.</returns>
        Task<ApiService> GetService(ApiKind kind, Version version=null, bool ignoreCache=false);

		/// <summary>
		/// Returns the Uri of an API Service. Only services with an active status will be considered valid for selection.
		/// </summary>
		/// <param name="kind">Kind of API.</param>
		/// <param name="version">Version. If null, the most recent version available will be returned.</param>
			/// <param name="ignoreCache">Wether the address stored in the local cache should be ignored.</param>
		/// <returns>And Uri or null if non matched the search criteria.</returns>
		/// <remarks>Helper method that internally calls <see cref="GetService"/>.</remarks>
        Task<Uri> GetServiceAddress(ApiKind kind, Version version=null, bool ignoreCache=false);

		/// <summary>
		/// Returns an APIs of a given kind.
		/// </summary>
		/// <param name="kind">Kind of API to be returned.</param>
			/// <param name="ignoreCache">Wether the address stored in the local cache should be ignored.</param>
		/// <returns>An Api instance or null if none of that kind was found.</returns>
        Task<Api> GetApi(ApiKind kind, bool ignoreCache=false);
    }
}
