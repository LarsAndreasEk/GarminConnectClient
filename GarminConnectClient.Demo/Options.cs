using System;
using CommandLine;
using CommandLine.Text;

namespace SuperRembo.GarminConnectClient.Demo
{
	sealed class Options : CommandLineOptionsBase
	{
		[Option("u", "UserName", HelpText = "Garmin Connect user name.", Required = true)]
		public string UserName { get; set; }

		[Option("p", "Password", HelpText = "Garmin Connect password.", Required = false)]
		public string Password { get; set; }

		[Option("a", "Action", Required = true)]
		public ProgramAction Action { get; set; }

		[Option("o", "OutputPath", DefaultValue = "")]
		public string OutputPath { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			var help = new HelpText
			{
				Heading = new HeadingInfo("Garmin Connect API Demo", ""),
				AdditionalNewLineAfterOption = true,
				AddDashesToOption = true
			};

			HandleParsingErrorsInHelp(help);
			help.AddPreOptionsLine("Usage: GarminConnectTool.exe -uJohnDoe -pPassword");
			help.AddOptions(this);

			return help;
		}

		void HandleParsingErrorsInHelp(HelpText help)
		{
			if (LastPostParsingState.Errors.Count <= 0) return;

			var errors = help.RenderParsingErrorsText(this, 2);
			if (string.IsNullOrEmpty(errors)) return;

			help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
			help.AddPreOptionsLine(errors);
		}

		public enum ProgramAction
		{
			Demo,
			ListAllActivities,
			ExportAllActivities
		}
	}

}
