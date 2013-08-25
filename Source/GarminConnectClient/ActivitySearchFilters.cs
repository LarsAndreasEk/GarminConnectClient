using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace GarminConnectClient
{
	public class ActivitySearchFilters
	{
		public string Keyword;// keyword
		public int? ActivityStartId;// start
		public int? Page;// currentPage
		public int? ActivitiesPerPage;// limit
		public bool? SortDescending;// sortOrder ASC or DESC
		public string SortField;// sortField
		public string Explore;// explore
		public bool? IgnoreNonGps;// ignoreNonGps
		public bool? IgnoreUntitled;// ignoreUntitled
		public string AggregateBy;// aggregateBy
		public DateTime? FromDate;// beginTimestamp
		public DateTime? ToDate;// endTimestamp

		public string ToQueryString()
		{
			var parameters = HttpUtils.CreateQueryString();
			parameters.AddIfNotNull("keyword", Keyword);
			parameters.AddIfNotNull("start", ActivityStartId);
			parameters.AddIfNotNull("currentPage", Page);
			parameters.AddIfNotNull("limit", ActivitiesPerPage);
			parameters.AddIfNotNull("sortField", SortField);
			parameters.AddIfNotNull("ignoreNonGps", IgnoreNonGps);
			parameters.AddIfNotNull("ignoreUntitled", IgnoreUntitled);
			parameters.AddIfNotNull("aggregateBy", AggregateBy);

			var specialParameters = new List<string>();
			if (FromDate.HasValue)
			{
				specialParameters.Add(String.Format("beginTimestamp>={0:s}", FromDate.Value));
			}
			if (ToDate.HasValue)
			{
				specialParameters.Add(String.Format("endTimestamp<={0:s}", ToDate.Value));
			}

			return CombineParametersToQueryString(parameters, specialParameters);
		}

		private string CombineParametersToQueryString(NameValueCollection parameters, List<string> specialParameters)
		{
			string queryString = parameters.ToString();
			if (specialParameters.Count == 0)
				return queryString;

			string specialQueryString = String.Join("&", specialParameters.ToArray());
			if (String.IsNullOrEmpty(queryString))
				return specialQueryString;

			return queryString + "&" + specialQueryString;
		}
	}

}