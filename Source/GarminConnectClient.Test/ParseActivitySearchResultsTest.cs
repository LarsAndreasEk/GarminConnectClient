using System.Linq;
using NUnit.Framework;
using GarminConnectClient.Data;

namespace GarminConnectClient.Test
{
	[TestFixture]
	class ParseActivitySearchResultsTest
	{
		[Test]
		public void ParseValidInput()
		{
			var json = GetActivitySearchResultsJson();
			var resultsContainer = ActivitySearchResultsContainer.ParseJson(json);
			Assert.IsNotNull(resultsContainer);

			var results = resultsContainer.Results;
			Assert.IsNotNull(results);
			Assert.AreEqual(171, results.TotalFound);
			Assert.AreEqual(1, results.CurrentPage);
			Assert.AreEqual(9, results.TotalPages);

			var activities = results.Activities;
			Assert.IsNotNull(activities);

			var firstActivityContainer = activities.FirstOrDefault();
			Assert.IsNotNull(firstActivityContainer);

			Activity firstActivity = firstActivityContainer.Activity;
			Assert.AreEqual(246164442, firstActivity.ActivityId);
			Assert.AreEqual("First activity", firstActivity.ActivityName);
			Assert.AreEqual("The first activity", firstActivity.ActivityDescription);

			var activityTypeContainer = firstActivity.ActivityType;
			Assert.IsNotNull(activityTypeContainer);
			Assert.AreEqual(ActivityType.Running, activityTypeContainer.Key);

			var eventTypeContainer = firstActivity.EventType;
			Assert.IsNotNull(eventTypeContainer);
			Assert.AreEqual(EventType.Training, eventTypeContainer.Key);

			var summary = firstActivity.ActivitySummary;
			Assert.IsNotNull(summary);
			Assert.IsNotNull(summary.SumDuration);
			Assert.AreEqual(6065.0, summary.SumDuration.Value);
			Assert.IsNotNull(summary.SumDistance);
			Assert.AreEqual(12.93, summary.SumDistance.Value);
			Assert.IsNotNull(summary.BeginTimestamp);
			Assert.AreEqual("2012-11-24T08:02:49", summary.BeginTimestamp.Value.ToString("s"));
		}

		private string GetActivitySearchResultsJson()
		{
			var resourceHelper = new EmbeddedResourceHelper(GetType().Assembly);
			return resourceHelper.GetResourceAsString("GarminConnectClient.Test.Data.ActivitiesSample1.json.txt");
		}
	}
}
