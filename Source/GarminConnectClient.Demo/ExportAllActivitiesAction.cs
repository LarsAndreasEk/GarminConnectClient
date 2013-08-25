using System;
using System.Collections.Generic;
using System.IO;
using GarminConnectClient.Data;

namespace GarminConnectClient.Demo
{
	class ExportAllActivitiesAction : ActionBase
	{
		public string OutputPath { get; set; }

		protected override void Action()
		{
			Setup();

			Console.WriteLine("Searching for activities...");

			List<Activity> activities = ActivitySearchService.FindAllActivities();
			if (activities == null)
			{
				Console.WriteLine("No activities found.");
				return;
			}

			Console.WriteLine("Total {0} activities found.", activities.Count);
			DownloadActivities(activities);
		}

		private void Setup()
		{
			Directory.CreateDirectory(OutputPath);
		}

		private void DownloadActivities(IEnumerable<Activity> activities)
		{
			foreach (var activity in activities)
			{
				DownloadActivity(activity);
			}
		}

		private void DownloadActivity(Activity activity)
		{
			string path = Path.Combine(OutputPath, activity.ActivityType.Key.ToString());
			Directory.CreateDirectory(path);
			string fileName = String.Format("Activity.{1:yyyyMMdd}.{0}.kml", activity.ActivityId, activity.ActivitySummary.BeginTimestamp.Value);
			string fullFileName = Path.Combine(path, fileName);
			ActivityService.Export(activity.ActivityId, fullFileName, ExportFileType.Kml);
			Console.WriteLine("Activity exported to {0}.", fullFileName);
		}
	}
}
