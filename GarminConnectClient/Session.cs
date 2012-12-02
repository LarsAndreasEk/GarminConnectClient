using System.Net;

namespace SuperRembo.GarminConnectClient
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
