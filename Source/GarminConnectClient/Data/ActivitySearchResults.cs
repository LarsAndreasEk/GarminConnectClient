using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GarminConnectClient.Data
{
	[DataContract]
	public class ActivitySearchResultsContainer
	{
		[DataMember(Name = "results")]
		public ActivitySearchResults Results { get; set; }

		public static ActivitySearchResultsContainer ParseJson(string json)
		{
			return JsonConvert.DeserializeObject<ActivitySearchResultsContainer>(json);
		}
	}

	[DataContract]
	public class ActivitySearchResults
	{
		[DataMember(Name = "activities")]
		public List<ActivityContainer> Activities { get; set; }

		[DataMember(Name = "totalFound")]
		public int TotalFound { get; set; }

		[DataMember(Name = "currentPage")]
		public int CurrentPage { get; set; }

		[DataMember(Name = "totalPages")]
		public int TotalPages { get; set; }

		public void Append(ActivitySearchResults results)
		{
			Activities.AddRange(results.Activities);
		}
	}
}
