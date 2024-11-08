using Loxone.Communicator;
using Loxone.Communicator.Extensions;
using Loxone.Communicator.Extensions.LoxApp3Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LxCommunicator.NET.Example.Http {
    internal class Program {
        private static HttpWebserviceClient client;

        private const string Serial = "";
        private const string Username = "";
        private const string Password = "";
        
        private static async Task Main(string[] args) {
	        var dnsLookupClient = new DnsLookupClient();
	        var dnsRecord = await dnsLookupClient.GetDnsRecord(Serial, CancellationToken.None);
	        
            using (client = new HttpWebserviceClient(dnsRecord.Host, dnsRecord.Port, 2, "098802e1-02b4-603c-ffffeee000d80cfd", "LxCommunicator.NET.Http", dnsRecord.HttpsOpen)) {
                using (TokenHandler handler = new TokenHandler(client, Username)) {
                    handler.SetPassword(Password);
                    await client.Authenticate(handler);

                    var loxApp3 = await client.GetLoxApp3();
                    var loxApp3Json = JsonConvert.SerializeObject(loxApp3);
                    File.WriteAllText("loxAPP3.json", loxApp3Json);
                    ListAllControls(loxApp3);

                    await handler.KillToken();
                    Console.ReadLine();
                }
            }
        }
        
        private static void ListAllControls(LoxApp3 loxApp3)
        {
	        foreach (var controlEntry in loxApp3.Controls)
	        {
		        string uuid = controlEntry.Key;
		        Control control = controlEntry.Value;

		        Console.WriteLine($"Control UUID: {uuid}");
		        Console.WriteLine($"Name: {control.Name}");
		        Console.WriteLine($"Type: {control.Type}");
		        Console.WriteLine($"Room: {loxApp3.Rooms[control.Room]?.Name}");
		        Console.WriteLine($"Category: {loxApp3.Categories[control.Category]?.Name}");
		        Console.WriteLine();
		        
		        if (control.SubControls != null)
		        {
			        foreach (var subControlEntry in control.SubControls)
			        {
				        Control subControl = subControlEntry.Value;
				        Console.WriteLine($"SubControl Name: {subControl.Name}, Type: {subControl.Type}");
			        }
		        }

		        var meter = control as Meter;
		        if (meter?.States != null)
		        {
			        foreach (var state in meter.States)
			        {
				        Console.WriteLine($"State Name: {state.Key}, UUID: {state.Value}");
			        }
		        }

		        Console.WriteLine("----------------------------");
	        }
        }
    }
}