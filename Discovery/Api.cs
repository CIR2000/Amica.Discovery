using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amica.vNext
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
	[JsonProperty("_id")]
	public string Id { get; internal set; }
	/// <summary>
	/// Kind of service provided.
	/// </summary>
	[JsonProperty("kind")]
        public ApiKind Kind { get; internal set; }
	/// <summary>
	/// Service name.
	/// </summary>
	[JsonProperty("name")]
	public string Name { get; internal set; }
	/// <summary>
	/// Service title.
	/// </summary>
	[JsonProperty("title")]
	public string Title { get; internal set; }
	/// <summary>
	/// Service description.
	/// </summary>
	[JsonProperty("description")]
	public string Description { get;  internal set; }
	/// <summary>
	/// Service owner.
	/// </summary>
	[JsonProperty("owner")]
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

