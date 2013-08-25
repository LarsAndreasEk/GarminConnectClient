using System;
using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	/// <example>
	///	"SumDuration": {
	///		"fieldDisplayName": "Time",
	///		"display": "01:41:05",
	///		"displayUnit": "Hours:Minutes:Seconds",
	///		"value": "6065.0",
	///		"unitAbbr": "h:m:s",
	///		"withUnit": "01:41:05 Hours:Minutes:Seconds",
	///		"withUnitAbbr": "01:41:05 h:m:s",
	///		"uom": "second"
	///	},
	/// </example>
	[DataContract]
	public class SumDuration
	{
		[DataMember(Name = "value")]
		public double Value { get; set; }

		public TimeSpan TimeValue
		{
			get { return TimeSpan.FromSeconds(Value); }
		}
	}
}
