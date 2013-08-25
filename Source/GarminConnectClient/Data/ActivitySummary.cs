using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{
	/// <example>
	///	"activitySummary": {
	///		"SumSampleCountElevation": { ... }
	///		"SumSampleCountSpeed": { ... }
	///		"SumSampleCountHeartRate": { ... }
	///		"SumSampleCountRunCadence": { ... }
	///		"MaxElevation": { ... }
	///		"EndLatitude": { ... }
	///		"EndLongitude": { ... }
	///		"WeightedMeanMovingSpeed": { ... }
	///		"WeightedMeanMovingPace": { ... }
	///		"MinRunCadence": { ... }
	///		"MaxHeartRate": { ... }
	///		"MinSpeed": { ... }
	///		"MinPace": { ... }
	///		"WeightedMeanHeartRate": { ... }
	///		"MaxSpeed": { ... }
	///		"MaxPace": { ... }
	///		"SumEnergy": { ... }
	///		"SumElapsedDuration": { ... }
	///		"MaxRunCadence": { ... }
	///		"WeightedMeanRunCadence": { ... }
	///		"BeginLatitude": { ... }
	///		"SumMovingDuration": { ... }
	///		"WeightedMeanSpeed": { ... }
	///		"WeightedMeanPace": { ... }
	///		"SumDuration": { ... }
	///		"SumDistance": { ... }
	///		"BeginLongitude": { ... }
	///		"SumSampleCountTimestamp": { ... }
	///		"MinHeartRate": { ... }
	///		"MinElevation": { ... }
	///		"GainElevation": { ... }
	///		"SumSampleCountLongitude": { ... }
	///		"BeginTimestamp": { ... }
	///		"EndTimestamp": { ... }
	///		"SumSampleCountLatitude": { ... }
	///		"LossElevation": { ... }
	///	},
	/// </example>
	[DataContract]
	public class ActivitySummary
	{
		[DataMember(Name = "SumDuration")]
		public SumDuration SumDuration { get; set; }

		[DataMember(Name = "SumDistance")]
		public SumDistance SumDistance { get; set; }

		[DataMember(Name = "BeginTimestamp")]
		public Timestamp BeginTimestamp { get; set; }

		[DataMember(Name = "EndTimestamp")]
		public Timestamp EndTimestamp { get; set; }
	}
}
