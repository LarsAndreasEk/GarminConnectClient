using System;
using CommandLine;

namespace GarminConnectClient.Demo
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var options = new Options();
			if (Parser.Default.ParseArguments(args, options))
			{
				switch (options.Action)
				{
					case Options.ProgramAction.Demo:
						(new DemoAction
						{
							UserName = options.UserName,
							Password = options.Password,
							OutputPath = options.OutputPath
						}).Run();
						break;

					case Options.ProgramAction.ListAllActivities:
						(new ListAllActivitiesAction
						{
							UserName = options.UserName,
							Password = options.Password
						}).Run();
						break;

					case Options.ProgramAction.ExportAllActivities:
						(new ExportAllActivitiesAction
						{
							UserName = options.UserName,
							Password = options.Password,
							OutputPath = options.OutputPath
						}).Run();
						break;
				}
			}

			ReadLineIfDebug();
		}

		private static void ReadLineIfDebug()
		{
#if (DEBUG)
			if (System.Diagnostics.Debugger.IsAttached)
				Console.ReadLine();
#endif
		}

	}
}
