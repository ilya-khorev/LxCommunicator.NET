using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class LoxApp3 
	{
		[JsonProperty("msInfo")]
		public MsInfo MsInfo { get; set; }

		[JsonProperty("globalStates")]
		public Dictionary<string, string> GlobalStates { get; set; }

		[JsonProperty("rooms")]
		public Dictionary<string, Room> Rooms { get; set; }

		[JsonProperty("cats")]
		public Dictionary<string, Category> Categories { get; set; }

		[JsonProperty("controls")]
		public Dictionary<string, Control> Controls { get; set; }

		[JsonProperty("operatingModes")]
		public OperatingModes OperatingModes { get; set; }

		[JsonProperty("weatherServer")]
		public WeatherServer WeatherServer { get; set; }
	}
}