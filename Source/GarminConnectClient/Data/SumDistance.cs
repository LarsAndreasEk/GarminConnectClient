using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	/// <example>
	/// "SumDistance": {
	///		"fieldDisplayName": "Distance",
	///		"display": "12.93",
	///		"displayUnit": "Kilometers",
	///		"value": "12.93",
	///		"unitAbbr": "km",
	///		"withUnit": "12.93 Kilometers",
	///		"withUnitAbbr": "12.93 km",
	///		"uom": "kilometer"
	///	},
	/// </example>
	[DataContract]
	public class SumDistance
	{
		[DataMember(Name = "value")]
		public double Value { get; set; }

		[DataMember(Name = "uom")]
		public string UnitOfMeasurement { get; set; }
	}
}
