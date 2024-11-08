using Newtonsoft.Json;

namespace Loxone.Communicator.Extensions.LoxApp3Models 
{
	public class MsInfo 
	{
		[JsonProperty("serialNr")]
		public string SerialNumber { get; set; }

		[JsonProperty("msName")]
		public string MsName { get; set; }

		[JsonProperty("projectName")]
		public string ProjectName { get; set; }

		[JsonProperty("localUrl")]
		public string LocalUrl { get; set; }

		[JsonProperty("remoteUrl")]
		public string RemoteUrl { get; set; }

		[JsonProperty("tempUnit")]
		public int TemperatureUnit { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("squareMeasure")]
		public string SquareMeasure { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }
		
		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		// Add other properties as needed
	}
}