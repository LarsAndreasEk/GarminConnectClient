using System;
using System.Collections.Generic;
using GarminConnectClient.Data;

namespace GarminConnectClient.Demo
{
	class ListAllActivitiesAction : ActionBase
	{
		protected override void Action()
		{
			Console.WriteLine("Searching for activities...");

			List<Activity> activities = ActivitySearchService.FindAllActivities();
			if (activities == null)
			{
				Console.WriteLine("No activities found.");
				return;
			}

			Console.WriteLine("Total {0} activities found.", activities.Count);
			foreach (var activity in activities)
			{
				DisplayActivity(activity);
			}
		}

		private void DisplayActivity(Activity activity)
		{
			ActivitySummary summary = activity.ActivitySummary;
			Console.WriteLine("Activity ID: {0}", activity.ActivityId);
			if (!String.IsNullOrEmpty(activity.ActivityName))
				Console.WriteLine("Name: {0}", activity.ActivityName);
			if (!String.IsNullOrEmpty(activity.ActivityDescription))
				Console.WriteLine("Description: {0}", activity.ActivityDescription);
			Console.WriteLine("Event type: {0}", activity.EventType);
			Console.WriteLine("Activity type: {0}", activity.ActivityType);
			if (summary.BeginTimestamp != null)
				Console.WriteLine("Begin: {0}", summary.BeginTimestamp.Value);
			if (summary.EndTimestamp != null)
				Console.WriteLine("End: {0}", summary.EndTimestamp.Value);
			if (summary.SumDuration != null)
				Console.WriteLine("Duration: {0}", summary.SumDuration.Value);
			if (summary.SumDistance != null)
				Console.WriteLine("Distance: {0}", summary.SumDistance.Value);
			Console.WriteLine();
		}
	}
}
