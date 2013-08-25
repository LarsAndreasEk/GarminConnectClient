using System.Net;

namespace GarminConnectClient
{
	public class Session
	{
		public CookieContainer Cookies { get; private set; }

		public Session()
		{
			Cookies = new CookieContainer();
		}
	}
}
