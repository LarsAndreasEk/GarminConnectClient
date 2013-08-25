using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace SuperRembo.GarminConnectClient
{
	public class SessionService
	{
		private const string SignInUrl = @"https://connect.garmin.com/signin";
		private const string DashboardUrl = @"http://connect.garmin.com/dashboard";

		public Session Session { get; private set; }

		public bool SignIn(string userName, string password)
		{
			try
			{
				Session = new Session();
				GetSignInPage(Session.Cookies);

				var signInResponse = PostSignInRequest(Session.Cookies, userName, password);

				if (IsDashboardUri(signInResponse.ResponseUri))
					return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error signing in. {0}", ex.Message);
			}

			return false;
		}

		public void SignOut()
		{
			var request = HttpUtils.CreateRequest(BuildSignOutUrl(), Session.Cookies);
			request.GetResponse();
		}

		private static string BuildSignOutUrl()
		{
			var queryString = HttpUtils.CreateQueryString();
			queryString.Add("actionMethod", "page/home/dashboard.xhtml:identity.logout");
			queryString.Add("cid", "");
			return String.Format("{0}?{1}", DashboardUrl, queryString);
		}

		private static void GetSignInPage(CookieContainer cookies)
		{
			var request = HttpUtils.CreateRequest(DashboardUrl, cookies);
			request.GetResponse();
		}

		private static HttpWebResponse PostSignInRequest(CookieContainer cookies, string userName, string password)
		{
			var formBinaryData = BuildSignInFormData(userName, password);

			var request = HttpUtils.CreateRequest(SignInUrl, cookies);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.WriteBinary(formBinaryData);
			return (HttpWebResponse)request.GetResponse();
		}

		private static byte[] BuildSignInFormData(string userName, string password)
		{
			var formParams = HttpUtils.CreateQueryString();
			formParams.Add("login", "login");
			formParams.Add("login:loginUsernameField", userName);
			formParams.Add("login:password", password);
			formParams.Add("login:signInButton", "Sign In");
			formParams.Add("javax.faces.ViewState", "j_id1");
			return Encoding.UTF8.GetBytes(formParams.ToString());
		}

		private static bool IsDashboardUri(Uri uri)
		{
			return uri.ToString().StartsWith(DashboardUrl);
		}
	}
}
