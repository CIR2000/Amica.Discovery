﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amica.vNext.Discovery
{
    interface IDiscovery
    {
	/// <summary>
	/// Returns an API Service of a given kind.
	/// </summary>
	/// <param name="kind">Kind of API.</param>
	/// <param name="version">Version. If null, the most recent version available will be returned.</param>
	/// <returns>An ApiService istance or null if none matched the search criteria.</returns>
        ApiService GetApiService(ApiKind kind, Version version=null);

	/// <summary>
	/// Returns the Uri of an API Service of a given kind.
	/// </summary>
	/// <param name="kind">Kind of API.</param>
	/// <param name="version">Version. If null, the most recent version available will be returned.</param>
	/// <returns>And Uri or null if non matched the search criteria.</returns>
	/// <remarks>Helper method that internally calls <see cref="GetApiService(ApiKind, Version)"/>.</remarks>
        Uri GetApiServiceAddress(ApiKind kind, Version version=null);

	/// <summary>
	/// Returns an API of a given kind.
	/// </summary>
	/// <param name="kind">Kind of API to be returned.</param>
	/// <returns>An Api instance or null if none of that kind was found.</returns>
        Api GetApi(ApiKind kind);
    }
}
