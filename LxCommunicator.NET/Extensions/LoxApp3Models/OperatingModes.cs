using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class OperatingModes 
	{
		[JsonProperty("modes")]
		public List<OperatingMode> Modes { get; set; }

		[JsonProperty("defaultMode")]
		public string DefaultMode { get; set; }

		// Add other properties as needed
	}
	
	public class OperatingMode
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("uuid")]
		public string Uuid { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }
	}
}