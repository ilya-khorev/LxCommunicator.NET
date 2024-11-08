using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loxone.Communicator.Extensions.LoxApp3Models {
	public class Meter: Control {
		[JsonProperty("details")]
		public MeterDetails Details { get; set; }

		[JsonProperty("states")]
		public Dictionary<MeterState, string> States { get; set; }
	}

	public class MeterDetails {
		/// <summary>
		/// unidirectional = default, if missing
		/// bidirectional
		/// storage
		/// </summary>
		public MeterType Type { get; set; }
		
		//0: regular
		public int DisplayType { get; set; }
		
		public string ActualFormat { get; set; }
		public string TotalFormat { get; set; }
		public string TotalFormatNeg { get; set; }
		public string StorageFormat { get; set; }
		public string StorageMax { get; set; }
		
		public string PowerName { get; set; }
	}

	public enum MeterType {
		Unidirectional,
		Bidirectional,
		Storage
	}
	
	public enum MeterState {
		Jlocked,
		Actual,
		Total,
		TotalNeg,
		Storage,
		TotalDay,
		TotalWeek,
		TotalMonth,
		TotalYear,
		TotalNegDay,
		TotalNegWeek,
		TotalNegMonth,
		TotalNegYear
	}
}