using Newtonsoft.Json;
using System;

namespace Loxone.Communicator {
	public class LxIpInfo {
		public int Code { get; set; }
		public string DataCenter { get; set; }

		[JsonProperty("DNS-Status")]
		public string DnsStatus { get; set; }

		[JsonProperty("IPHTTPS")]
		public string IpHttps { get; set; }
		
		[JsonIgnore]
		public Uri Uri => new Uri("http://" + IpHttps);
		
		[JsonProperty("PortOpenHTTPS")]
		public bool PortOpenHttps { get; set; }

		public bool RemoteConnect { get; set; }
	}
}