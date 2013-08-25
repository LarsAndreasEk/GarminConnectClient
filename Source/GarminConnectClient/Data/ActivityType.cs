using System.Runtime.Serialization;

namespace GarminConnectClient.Data
{

	/// <example>
	/// "activityType": {
	///		"display": "Running",
	///		"key": "running",
	///		"fieldNameDisplay": "Activity Type",
	///		"parent": {
	///			"display": "Running",
	///			"key": "running",
	///			"fieldNameDisplay": "Activity Type"
	///		}
	///	},
	/// </example>
	[DataContract]
	public class ActivityTypeContainer
	{
		[DataMember(Name = "key")]
		public ActivityType Key { get; set; }
	}

	public enum ActivityType
	{
		All,
		Uncategorized,
		Running,
		Street_running,
		Track_running,
		Trail_running,
		Treadmill_running,
		Cycling,
		Cyclocross,
		Downhill_biking,
		Indoor_cycling,
		Mountain_biking,
		Recumbent_cycling,
		Road_biking,
		Track_cycling,
		Fitness_equipment,
		Elliptical,
		Indoor_cardio,
		Indoor_rowing,
		Stair_climbing,
		Strength_training,
		Hiking,
		Swimming,
		Lap_swimming,
		Open_water_swimming,
		Walking,
		Casual_walking,
		Speed_walking,
		Transition,
		SwimToBikeTransition,
		BikeToRunTransition,
		RunToBikeTransition,
		Motorcycling,
		Other,
		Backcountry_skiing_snowboarding,
		Boating,
		Cross_country_skiing,
		Driving_general,
		Flying,
		Golf,
		Horseback_riding,
		Inline_skating,
		Mountaineering,
		Paddling,
		Resort_skiing_snowboarding,
		Rowing,
		Sailing,
		Skate_skiing,
		Skating,
		Snowmobiling,
		Snow_shoe,
		Stand_up_paddleboarding,
		Whitewater_rafting_kayaking,
		Wind_kite_surfing,
	}

}
