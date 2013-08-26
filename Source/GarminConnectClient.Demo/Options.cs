using CommandLine;
using CommandLine.Text;

namespace GarminConnectClient.Demo
{
	sealed class Options
	{
		[Option('u', "UserName", Required = true, HelpText = "Garmin Connect user name.")]
		public string UserName { get; set; }

		[Option('p', "Password", Required = false, HelpText = "Garmin Connect password.")]
		public string Password { get; set; }

		[Option('a', "Action", Required = true, HelpText = "Demo, ListAllActivities or ExportAllActivities.")]
		public ProgramAction Action { get; set; }

		[Option('o', "OutputPath", DefaultValue = "")]
		public string OutputPath { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this,
				current => HelpText.DefaultParsingErrorsHandler(this, current));
		}

		public enum ProgramAction
		{
			Demo,
			ListAllActivities,
			ExportAllActivities
		}
	}

}
