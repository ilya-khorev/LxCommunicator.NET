using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class Control 
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("uuidAction")]
		public string UuidAction { get; set; }

		[JsonProperty("room")]
		public string Room { get; set; }

		[JsonProperty("cat")]
		public string Category { get; set; }

		[JsonProperty("defaultRating")]
		public int DefaultRating { get; set; }

		[JsonProperty("isFavorite")]
		public bool IsFavorite { get; set; }

		[JsonProperty("isSecured")]
		public bool IsSecured { get; set; }

		[JsonProperty("subControls")]
		public Dictionary<string, Control> SubControls { get; set; }

		// Add other properties specific to the control type
	}
}