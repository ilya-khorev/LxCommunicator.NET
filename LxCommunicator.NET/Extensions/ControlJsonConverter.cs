using Loxone.Communicator.Extensions.LoxApp3Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Loxone.Communicator.Extensions {
	public class ControlJsonConverter : JsonConverter {
		public override bool CanConvert(Type objectType) {
			return typeof(Control).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			JObject jo = JObject.Load(reader);
			string type = jo["type"]?.ToString();

			Control control;

			switch (type) {
				case "Meter":
					control = new Meter();
					break;
				// Handle other control types as needed
				default:
					control = new Control();
					break;
			}

			serializer.Populate(jo.CreateReader(), control);
			return control;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			// Implement if serialization is needed
			throw new NotImplementedException();
		}
	}
}