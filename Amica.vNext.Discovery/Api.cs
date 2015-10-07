using System.Collections.Generic;

namespace Amica.vNext.Discovery
{
    public class Api
    {
        public Api()
        {
            Services = new List<ApiService>();
        }
	/// <summary>
	/// Unique service identifier.
	/// </summary>
	public string Id { get; internal set; }
	/// <summary>
	/// Kind of service provided.
	/// </summary>
        public ApiKind Kind { get; internal set; }
	/// <summary>
	/// Service name.
	/// </summary>
	public string Name { get; internal set; }
	/// <summary>
	/// Service title.
	/// </summary>
	public string Title { get; internal set; }
	/// <summary>
	/// Service description.
	/// </summary>
	public string Description { get;  internal set; }
	/// <summary>
	/// Service owner.
	/// </summary>
	public Owner Owner { get; internal set; }

        public List<ApiService> Services { get; }
    }

    public enum ApiKind
    {
        Authentication,
	Discovery,
	UserData

    }
}

