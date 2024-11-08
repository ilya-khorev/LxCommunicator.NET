using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class WeatherServer 
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("serverType")]
		public string ServerType { get; set; }

		[JsonProperty("uuid")]
		public string Uuid { get; set; }

		[JsonProperty("states")]
		public Dictionary<string, string> States { get; set; }

		// Add other properties as needed
	}
}