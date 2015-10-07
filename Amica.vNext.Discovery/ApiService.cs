using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amica.vNext.Discovery
{
    public class ApiService
    {
	/// <summary>
	///  Service Uri.
	///  </summary>
        public Uri BaseAddress { get; internal set; }
	/// <summary>
	/// Current status of the service.
	/// </summary>
	public ServiceStatus ServiceStatus { get; internal set; }
	/// <summary>
	/// Documentation Uri.
	/// </summary>
	public Uri Documentation { get; internal set; }
	/// <summary>
	/// Service version.
	/// </summary>
	public Version Version { get; internal set; }
	/// <summary>
	/// Kind of authentication required.
	/// </summary>
	public AuthenticationKind Authentication { get; internal set; }
	/// <summary>
	/// Wether the service is deprecated or not.
	/// </summary>
	public bool Deprecated { get; internal set; }
	/// <summary>
	/// Uri to the corresponding discovery document.
	/// </summary>
	public Uri Discovery { get; internal set; }

    }

    public enum ServiceStatus
    {
        Active,
        Inactive,
	Suspended
    }

    public enum AuthenticationKind
    {
        BearerToken,
	Basic,
	None
    }
}
