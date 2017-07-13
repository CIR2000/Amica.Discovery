using System;
using Newtonsoft.Json;

namespace Amica.Discovery
{
    public class ApiService
    {
	/// <summary>
	///  Service Uri.
	///  </summary>
	[JsonProperty("base_address")]
	public Uri BaseAddress { get; internal set; }
	/// <summary>
	/// Current status of the service.
	/// </summary>
	[JsonProperty("status")]
	public ServiceStatus ServiceStatus { get; internal set; }
	/// <summary>
	/// Documentation Uri.
	/// </summary>
	[JsonProperty("documentation")]
	public Uri Documentation { get; internal set; }
	/// <summary>
	/// Service version.
	/// </summary>
	[JsonProperty("version")]
	public Version Version { get; internal set; }
	/// <summary>
	/// Kind of authentication required.
	/// </summary>
	[JsonProperty("authentication")]
	public AuthenticationKind Authentication { get; internal set; }
	/// <summary>
	/// Wether the service is deprecated or not.
	/// </summary>
	[JsonProperty("deprecated")]
	public bool Deprecated { get; internal set; }
	/// <summary>
	/// Uri to the corresponding discovery document.
	/// </summary>
	[JsonProperty("discovery")]
	public Uri Discovery { get; internal set; }

    }

    public enum ServiceStatus
    {
        Active,
        Inactive,
		Suspended,
		Undetermined
    }

    public enum AuthenticationKind
    {
        BearerToken,
		Basic,
		None
    }
}
