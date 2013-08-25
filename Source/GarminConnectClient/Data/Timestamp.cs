using System;
using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	/// <example>
	/// "BeginTimestamp": {
	///		"fieldDisplayName": "Start",
	///		"display": "Sat, 24 Nov 2012 9:02",
	///		"displayUnit": "",
	///		"value": "2012-11-24T08:02:49.000Z",
	///		"unitAbbr": "Central European Time",
	///		"withUnit": "Sat, 24 Nov 2012 9:02 ",
	///		"withUnitAbbr": "Sat, 24 Nov 2012 9:02 Central European Time",
	///		"uom": "Europe/Paris"
	///	},
	/// </example>
	[DataContract]
	public class Timestamp
	{
		[DataMember(Name = "value")]
		public DateTime Value { get; set; }
	}
}
