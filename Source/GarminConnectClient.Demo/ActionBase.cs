using System;

namespace GarminConnectClient.Demo
{
	internal abstract class ActionBase
	{
		private SessionService sessionService;
		private ActivitySearchService activitySearchService;
		private ActivityService activityService;

		protected SessionService SessionService
		{
			get { return sessionService = sessionService ?? new SessionService(); }
		}

		protected ActivitySearchService ActivitySearchService
		{
			get { return activitySearchService = activitySearchService ?? new ActivitySearchService(SessionService.Session); }
		}

		protected ActivityService ActivityService
		{
			get { return activityService = activityService ?? new ActivityService(SessionService.Session); }
		}

		public string UserName { get; set; }
		public string Password { get; set; }

		public void Run()
		{
			if (!TrySignIn()) return;
			Action();
			SignOut();
		}

		protected bool TrySignIn()
		{
			PromptForCredentialsIfNotPresent();

			Console.WriteLine("Signing in...");
			bool signedIn = SessionService.SignIn(UserName, Password);
			if (!signedIn)
			{
				Console.WriteLine("Failed to sign in as {0}.", UserName);
				return false;
			}

			Console.WriteLine("Signed in as {0}.", UserName);
			return true;
		}

		private void PromptForCredentialsIfNotPresent()
		{
			if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password))
			{
				if (!String.IsNullOrEmpty(UserName))
				{
					Console.WriteLine("User name: {0}", UserName);
				}
				else
				{
					Console.Write("User name: ");
					UserName = Console.ReadLine();
				}

				Console.Write("Password: ");
				Password = ConsoleHelper.ReadPassword();
			}
		}

		protected void SignOut()
		{
			SessionService.SignOut();
			Console.WriteLine("Signed out.");
		}

		protected abstract void Action();

		public static string ReadPassword()
		{
			string password = "";
			ConsoleKeyInfo info = Console.ReadKey(true);
			while (info.Key != ConsoleKey.Enter)
			{
				if (info.Key != ConsoleKey.Backspace)
				{
					Console.Write("*");
					password += info.KeyChar;
				}
				else if (info.Key == ConsoleKey.Backspace)
				{
					if (!String.IsNullOrEmpty(password))
					{
						// remove one character from the list of password characters
						password = password.Substring(0, password.Length - 1);
						// get the location of the cursor
						int pos = Console.CursorLeft;
						// move the cursor to the left by one character
						Console.SetCursorPosition(pos - 1, Console.CursorTop);
						// replace it with space
						Console.Write(" ");
						// move the cursor to the left by one character again
						Console.SetCursorPosition(pos - 1, Console.CursorTop);
					}
				}
				info = Console.ReadKey(true);
			}

			// add a new line because user pressed enter at the end of their password
			Console.WriteLine();
			return password;
		}

	}
}