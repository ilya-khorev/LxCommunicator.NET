using Newtonsoft.Json;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class Room 
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("image")]
		public string Image { get; set; }

		[JsonProperty("uuid")]
		public string Uuid { get; set; }

		[JsonProperty("defaultRating")]
		public int DefaultRating { get; set; }

		// Add other properties as needed
	}
}