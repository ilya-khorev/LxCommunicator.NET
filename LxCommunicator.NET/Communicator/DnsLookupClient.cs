using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Loxone.Communicator 
{
	public class DnsLookupClient 
	{
		public async Task<DnsLookupResult> GetDnsRecord(string serial, CancellationToken cancellationToken) 
		{
			serial = serial.Replace(":", "").Trim().ToUpper();
			using (HttpClient httpClient = new HttpClient()) {
				HttpResponseMessage ipResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, 
						$"http://dns.loxonecloud.com/?getip&snr={serial}&json=true"), cancellationToken);
				ipResponse.EnsureSuccessStatusCode();
				var response = await ipResponse.Content.ReadAsStringAsync();
				LxIpInfo ipInfo = JsonConvert.DeserializeObject<LxIpInfo>(response);

				// Clean up the {ip} → {cleaned-ip}
				// i. IPv4: Replace dots (“.”) with minuses (“-”)
				// ii. IPv6: Replace colons (“:”) with minuses (“-”) and remove the brackets (“[“ &
				// 	“]”) at the beginning and end.
				// 	c. Create hostname “{cleaned-ip}.{snr}.dyndns.loxonecloud.com:{port}”
				// i. {snr} is your Miniservers Serial-Number.
				// 	ii. E.g.: “200-12-14-24.{snr}.dyndns.loxonecloud.com:4523”

				var uri = ipInfo.Uri;
				var host = uri.Host
					.Replace(".", "-")
					.Replace(":", "-")
					.Replace("[", "")
					.Replace("]", "");

				host = $"{host}.{serial}.dyndns.{ipInfo.DataCenter}";

				return new DnsLookupResult { Host = host, HttpsOpen = ipInfo.PortOpenHttps, Port = uri.Port };
			}
		}

		public class DnsLookupResult 
		{
			public string Host { get; set; }
			public int Port { get; set; }
			public bool HttpsOpen { get; set; }
		}
	}
}