using Loxone.Communicator;
using Loxone.Communicator.Events;
using Loxone.Communicator.Extensions;
using Loxone.Communicator.Extensions.LoxApp3Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LxCommunicator.NET.Example.Websocket {
	internal class Program {
		private static WebsocketWebserviceClient client;

		private const string Serial = "";
		private const string Username = "";
		private const string Password = "";

		private static LoxApp3 loxApp3;
		private static Dictionary<string, (Meter, MeterState)> meterStates;

		private static async Task Main(string[] args) {
			var dnsLookupClient = new DnsLookupClient();
			var dnsRecord = await dnsLookupClient.GetDnsRecord(Serial, CancellationToken.None);

			using (var webserviceClient = new HttpWebserviceClient(dnsRecord.Host, dnsRecord.Port, 2, "098802e1-02b4-603c-ffffeee000d80cfd",
				       "LxCommunicator.NET.Http",
				       dnsRecord.HttpsOpen)) {
				using (TokenHandler handler = new TokenHandler(webserviceClient, Username)) {
					handler.SetPassword(Password);
					await webserviceClient.Authenticate(handler);
					loxApp3 = await webserviceClient.GetLoxApp3();
					PrepareMeterStatesDictionary();
				}
			}

			using (client = new WebsocketWebserviceClient(dnsRecord.Host, dnsRecord.Port, 2, "098802e1-02b4-603c-ffffeee000d80cfd",
				       "LxCommunicator.NET.Websocket", dnsRecord.HttpsOpen)) {
				using (TokenHandler handler = new TokenHandler(client, Username)) {
					handler.SetPassword(Password);
					client.OnReceiveEventTable += Client_OnReceiveEventTable;
					client.OnAuthenticated += Client_OnAuthenticated;
					await client.Authenticate(handler);

					string result = (await client.SendWebservice(new WebserviceRequest<string>("jdev/sps/enablebinstatusupdate", EncryptionType.None))).Value;
					Console.ReadLine();
					await handler.KillToken();
				}
			}
		}

		private static void PrepareMeterStatesDictionary() {
			meterStates = new Dictionary<string, (Meter, MeterState)>();
			foreach (var controlEntry in loxApp3.Controls) {
				var control = controlEntry.Value;
				var meter = control as Meter;
				if (meter == null)
					continue;

				foreach (var state in meter.States) {
					meterStates.Add(state.Value.Replace("-", ""), (meter, state.Key));
				}
			}
		}

		private static void Client_OnAuthenticated(object sender, ConnectionAuthenticatedEventArgs e) {
			Console.WriteLine("Successfully authenticated!");
		}

		private static void Client_OnReceiveEventTable(object sender, EventStatesParsedEventArgs e) {
			foreach (EventState state in e.States) {
				if (state is ValueState valueState) {
					if (meterStates.TryGetValue(state.Uuid.ToString().Replace("-", ""), out var meterState)) {
						Console.WriteLine("{0}, {1}, Value={2}", meterState.Item1.Name, meterState.Item2.ToString(), valueState.Value);
					}
					else {
						Console.WriteLine(state.ToString());
					}
				}
				else {
					Console.WriteLine(state.ToString());
				}
			}
		}
	}
}