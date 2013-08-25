using System;
using System.Net;

namespace GarminConnectClient
{
	public class ActivityService
	{
		private readonly Session session;

		public ActivityService(Session session)
		{
			this.session = session;
		}

		public void Export(int activityId, string fileName, ExportFileType fileType)
		{
			Export(activityId, fileName, fileType, true, false);
		}

		public void Export(int activityId, string fileName, ExportFileType fileType, bool detailedHistory, bool original)
		{
			string url = BuildExportUrl(fileType, activityId, detailedHistory, original);
			var request = HttpUtils.CreateRequest(url, session.Cookies);
			var response = (HttpWebResponse)request.GetResponse();
			response.SaveResponseToFile(fileName);
		}

		private static string BuildExportUrl(ExportFileType fileType, int activityId, bool detailedHistory, bool original)
		{
			var queryString = HttpUtils.CreateQueryString();
			queryString.Add("full", detailedHistory.ToString().ToLower());
			queryString.Add("original", original.ToString().ToLower());
			return String.Format("http://connect.garmin.com/proxy/activity-service-1.2/{0}/activity/{1}?{2}",
				fileType.ToString().ToLower(), activityId, queryString);
		}
	}
}
