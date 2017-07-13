﻿using System;
using Newtonsoft.Json;

namespace Amica.Discovery
{
    public class Owner
    {
	[JsonProperty("name")]
	public string Name { get; set; }
	[JsonProperty("uri")]
	public Uri Uri { get; set; }
	[JsonProperty("contact")]
	public string Contact { get; set; }
    }
}
