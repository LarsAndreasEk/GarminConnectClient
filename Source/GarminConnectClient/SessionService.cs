using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace GarminConnectClient
{
	public class SessionService
	{
		private const string ClientId = "SuperRembo-GarminConnectClient";

		public Session Session { get; private set; }

		public bool SignIn(string userName, string password)
		{
			try
			{
				Session = new Session();

				var key = GetFlowExecutionKey();
				var signInResponse = PostLogInRequest(userName, password, key);
				var ticketUrl = GetServiceTicketUrl(signInResponse);
				return ProcessTicket(ticketUrl);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error signing in. {0}", ex.Message);
			}

			return false;
		}

		private string GetFlowExecutionKey()
		{
			var request = HttpUtils.CreateRequest(GetLogInUrl(), Session.Cookies);
			var response = (HttpWebResponse)request.GetResponse();
			var content = response.GetResponseAsString();
			return ParseFlowExecutionKey(content);
		}

		private static string ParseFlowExecutionKey(string content)
		{
			// <!-- flowExecutionKey: [XXXX] -->
			var re = new Regex(@"\bflowExecutionKey:\s*\[(?<key>[^]]*)\]");
			var m = re.Match(content);
			if (!m.Success)
				throw new Exception("FlowExecutionKey not found.");

			return m.Groups["key"].Value;
		}

		private HttpWebResponse PostLogInRequest(string userName, string password, string key)
		{
			var request = HttpUtils.CreateRequest(GetLogInUrl(), Session.Cookies);
			request.WriteFormData(BuildLogInFormData(userName, password, key));
			return (HttpWebResponse)request.GetResponse();
		}

		private static string GetLogInUrl()
		{
			var qs = HttpUtils.CreateQueryString();
			qs.Add("service", "http://connect.garmin.com/post-auth/login");
			qs.Add("clientId", ClientId);
			//qs.Add("webhost", "olaxpw-connect02.garmin.com");
			//qs.Add("source", "http://connect.garmin.com/en-US/signin");
			//qs.Add("redirectAfterAccountLoginUrl", "http://connect.garmin.com/post-auth/login");
			//qs.Add("redirectAfterAccountCreationUrl", "http://connect.garmin.com/post-auth/login");
			//qs.Add("gauthHost", "https://sso.garmin.com/sso");
			//qs.Add("locale", "en_US");
			//qs.Add("id", "gauth-widget");
			//qs.Add("cssUrl", "https://static.garmincdn.com/com.garmin.connect/ui/src-css/gauth-custom.css");
			//qs.Add("clientId", "GarminConnect");
			//qs.Add("rememberMeShown", "true");
			//qs.Add("rememberMeChecked", "false");
			//qs.Add("createAccountShown", "true");
			//qs.Add("openCreateAccount", "false");
			//qs.Add("usernameShown", "true");
			//qs.Add("displayNameShown", "false");
			//qs.Add("consumeServiceTicket", "false");
			//qs.Add("initialFocus", "true");
			//qs.Add("embedWidget", "false");

			return "https://sso.garmin.com/sso/login?" + qs;
		}

		private static NameValueCollection BuildLogInFormData(string userName, string password, string key)
		{
			var data = HttpUtils.CreateQueryString();
			data.Add("username", userName);
			data.Add("password", password);
			data.Add("embed", "true");
			data.Add("lt", key);
			data.Add("_eventId", "submit");
			//data.Add("displayNameRequired", "false");
			//data.Add("rememberme", "on");
			return data;
		}

		private bool ProcessTicket(string ticketUrl)
		{
			var request = HttpUtils.CreateRequest(ticketUrl, Session.Cookies);
			var response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception("Invalid ticket URL.");

			return IsDashboardUri(response.ResponseUri);
		}

		private static bool IsDashboardUri(Uri uri)
		{
			return uri.Host == "connect.garmin.com"
				&& uri.LocalPath == "/dashboard";
		}

		private string GetServiceTicketUrl(HttpWebResponse signInResponse)
		{
			var content = signInResponse.GetResponseAsString();
			return ParseServiceTicketUrl(content);
		}

		private static string ParseServiceTicketUrl(string content)
		{
			// var response_url                 = 'http://connect.garmin.com/post-auth/login?ticket=ST-XXXXXX-XXXXXXXXXXXXXXXXXXXX-cas';
			var re = new Regex(@"response_url\s*=\s*'(?<url>[^']*)'");
			var m = re.Match(content);
			if (!m.Success)
				throw new Exception("Servcie ticket URL not found.");

			return m.Groups["url"].Value;
		}

		public void SignOut()
		{
			var request = HttpUtils.CreateRequest("https://sso.garmin.com/sso/logout?service=http%3A%2F%2Fconnect.garmin.com%2F", Session.Cookies);
			request.GetResponse();
		}
	}
}
