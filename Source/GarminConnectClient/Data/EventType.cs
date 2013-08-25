using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	/// <example>        
	/// "eventType": {
	///		"display": "Training",
	///		"key": "training",
	///		"fieldNameDisplay": "Event Type"
	///	},
	///</example>
	[DataContract]
	public class EventTypeContainer
	{
		[DataMember(Name = "key")]
		public EventType Key { get; set; }
	}

	public enum EventType
	{
		All,
		Geocaching,
		Fitness,
		Recreation,
		Race,
		SpecialEvent,
		Training,
		Transportation,
		Touring,
		Uncategorized
	}
}
