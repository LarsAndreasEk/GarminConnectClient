using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	[DataContract]
	public class ActivityContainer
	{
		[DataMember(Name = "activity")]
		public Activity Activity { get; set; }
	}

	[DataContract]
	public class Activity
	{
		[DataMember(Name = "activityId")]
		public int ActivityId { get; set; }

		[DataMember(Name = "activityName")]
		public string ActivityName { get; set; }

		[DataMember(Name = "activityDescription")]
		public string ActivityDescription { get; set; }

		[DataMember(Name = "activityType")]
		public ActivityTypeContainer ActivityType { get; set; }

		[DataMember(Name = "eventType")]
		public EventTypeContainer EventType { get; set; }

		[DataMember(Name = "activitySummary")]
		public ActivitySummary ActivitySummary { get; set; }
	}
}