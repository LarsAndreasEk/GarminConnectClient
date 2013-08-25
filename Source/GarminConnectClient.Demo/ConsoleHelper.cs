using System;

namespace GarminConnectClient.Demo
{
	static class ConsoleHelper
	{
		public static string ReadPassword()
		{
			const char passwordChar = '*';
			string password = String.Empty;

			ConsoleKeyInfo info;
			while ((info = Console.ReadKey(true)).Key != ConsoleKey.Enter)
			{
				if (info.Key != ConsoleKey.Backspace)
				{
					password += info.KeyChar;
					Console.Write(passwordChar);
				}
				else if (password.Length > 0)
				{
					password = password.Remove(password.Length - 1);
					Console.Write("\b \b");
				}
			}

			Console.WriteLine();
			return password;
		}
	}
}
