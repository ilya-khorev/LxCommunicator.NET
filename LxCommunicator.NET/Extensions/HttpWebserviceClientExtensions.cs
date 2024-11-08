using Loxone.Communicator.Extensions.LoxApp3Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loxone.Communicator.Extensions {
	public static class HttpWebserviceClientExtensions {
		public static async Task<LoxApp3> GetLoxApp3(this HttpWebserviceClient client, EncryptionType encryptionType = EncryptionType.None) {
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				Converters = new List<JsonConverter> { new ControlJsonConverter() },
				NullValueHandling = NullValueHandling.Ignore
			};
			
			return await GetResponse<LoxApp3>(client, "data/LoxAPP3.json", settings, encryptionType);
		}
		
		public static async Task<WebserviceContent<double>> GetContolState(this HttpWebserviceClient client, string controlUuid, EncryptionType encryptionType = EncryptionType.None) {
			return await GetValueResponse<double>(client, $"jdev/sps/io/{controlUuid}/get", encryptionType);
		}

		private static async Task<T> GetResponse<T>(HttpWebserviceClient client, string command, JsonSerializerSettings settings, EncryptionType encryptionType = EncryptionType.None) {
			var json = await GetStringResponse(client, command, encryptionType);
			return JsonConvert.DeserializeObject<T>(json, settings);
		}

		private static async Task<string> GetStringResponse(HttpWebserviceClient client, string command, EncryptionType encryptionType = EncryptionType.None) {
			WebserviceResponse response = await client.SendWebservice(new WebserviceRequest(command, encryptionType));
			return response.GetAsStringContent();
		}
		
		
		private static async Task<WebserviceContent<T>> GetValueResponse<T>(HttpWebserviceClient client, string command, EncryptionType encryptionType = EncryptionType.None) {
			WebserviceResponse response = await client.SendWebservice(new WebserviceRequest(command, encryptionType));
			return response.GetAsWebserviceContent<T>();
		}

		public static string ToJson<T>(T obj) {
			return JsonConvert.SerializeObject(obj);
		}
		
		public static string PrettifyJson(string json) {
			JObject jsonObject = JObject.Parse(json);
			return jsonObject.ToString(Formatting.Indented);
		}
	}
}