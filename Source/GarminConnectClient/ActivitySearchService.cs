using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using GarminConnectClient.Data;

namespace GarminConnectClient
{
	public class ActivitySearchService
	{
		private readonly Session session;

		public ActivitySearchService(Session session)
		{
			this.session = session;
		}

		public ActivitySearchResultsContainer FindActivities()
		{
			return FindActivities(new ActivitySearchFilters());
		}

		public ActivitySearchResultsContainer FindActivities(ActivitySearchFilters filters)
		{
			string url = BuildSearchUrl(filters);
			Debug.WriteLine("FindActivities: {0}", (object)url);
			var request = HttpUtils.CreateRequest(url, session.Cookies);
			var response = (HttpWebResponse)request.GetResponse();
			string responseText = response.GetResponseAsString();
			return ActivitySearchResultsContainer.ParseJson(responseText);
		}


		public List<Activity> FindAllActivities()
		{
			return FindAllActivities(new ActivitySearchFilters());
		}

		public List<Activity> FindAllActivities(ActivitySearchFilters filters)
		{
			filters.Page = 0;

			var activities = new List<Activity>();
			ActivitySearchResultsContainer results;
			do
			{
				filters.Page++;
				Debug.WriteLine("Searching page {0}", filters.Page);
				results = FindActivities(filters);
				activities.AddRange(results.Results.Activities.Select(a => a.Activity));
				Debug.WriteLine("Found page {0} or {1}", results.Results.CurrentPage, results.Results.TotalPages);
			} while (results.Results.CurrentPage < results.Results.TotalPages);

			return activities;
		}

		private static string BuildSearchUrl(ActivitySearchFilters filters)
		{
			// Example URL
			// http://connect.garmin.com/proxy/activity-search-service-1.2/json/activities?currentPage=1&sortOrder=DESC&limit=100
			// http://connect.garmin.com/proxy/activity-search-service-1.2/json/activities?currentPage=1&sortOrder=DESC&limit=100&beginTimestamp%3E=2012-11-24T03:00:00

			const string serviceUrl = "http://connect.garmin.com/proxy/activity-search-service-1.2/json/activities";

			var queryString = filters.ToQueryString();
			return String.Format("{0}?{1}", serviceUrl, queryString);
		}

	}
}
