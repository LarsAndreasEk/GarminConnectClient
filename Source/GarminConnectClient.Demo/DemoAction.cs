using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GarminConnectClient.Data;

namespace GarminConnectClient.Demo
{
	class DemoAction : ActionBase
	{
		public string OutputPath { get; set; }

		public DemoAction()
		{
			OutputPath = String.Empty;
		}

		protected override void Action()
		{
			var activities = GetAllActivities();

			Activity firstActivity = activities.FirstOrDefault();
			if (firstActivity != null)
				ExportActivityFiles(firstActivity);
		}

		private IEnumerable<Activity> GetAllActivities()
		{
			Console.WriteLine("Search for activities in the past month...");
			var searchOptions = new ActivitySearchFilters
			{
				FromDate = DateTime.Today.AddMonths(-1)
			};

			List<Activity> activities = ActivitySearchService.FindAllActivities(searchOptions);
			if (activities == null)
			{
				Console.WriteLine("No activities found.");
				return null;
			}

			Console.WriteLine("Total {0} activities found.", activities.Count);
			foreach (var activity in activities)
			{
				DisplayActivity(activity);
			}

			return activities;
		}

		private void DisplayActivity(Activity activity)
		{
			Console.WriteLine("Activity {0}: {1}, {2}", activity.ActivityId, activity.ActivityName, activity.ActivityDescription);
			ActivitySummary summary = activity.ActivitySummary;
			Console.WriteLine("Begin: {0}, duration: {1}, distance {2}", summary.BeginTimestamp.Value, summary.SumDuration.Value, summary.SumDistance.Value);
		}

		private void ExportActivityFiles(Activity activity)
		{
			ExportActivityFileForType(activity, ExportFileType.Axm);
			ExportActivityFileForType(activity, ExportFileType.Kml);
			ExportActivityFileForType(activity, ExportFileType.Gpx);
			ExportActivityFileForType(activity, ExportFileType.Tcx);
			ExportActivityFileForType(activity, ExportFileType.PolyLine);
			ExportActivityFileForType(activity, ExportFileType.GPolyLine);
		}

		private void ExportActivityFileForType(Activity activity, ExportFileType fileType)
		{
			string fileName = BuildActivityFileName(activity.ActivityId, fileType);
			Console.WriteLine("Downloading activity as {0}...", fileType);
			ActivityService.Export(activity.ActivityId, fileName, fileType);
			Console.WriteLine("Downloaded activity to {0}", fileName);
		}

		private string BuildActivityFileName(int activityId, ExportFileType fileType)
		{
			string fileName = String.Format("Activity{0}.{1}", activityId, fileType.ToString().ToLower());
			return Path.Combine(OutputPath, fileName);
		}
	}
}
