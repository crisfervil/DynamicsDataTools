﻿using CommandLine;

namespace XrmCommandBox
{
    public class CommonOptions
    {
        [Option("connection", Required = true, HelpText ="Connection string, or name of a connection string to use")]
        public string ConnectionName { get; set; }

        [Option("debug-brk", HelpText = "Launches the debugger before running the selected command")]
        public bool DebugBreak { get; set; }

        [Option("config-file", HelpText = "Xml file containing the command options")]
        public string ConfigFile { get; set; }

        [Option("continue-on-error", HelpText = "Continue if there's an error while processing the command")]
        public bool ContinueOnError { get; set; }

        [Option("log-level", HelpText = "Sets the current logging output. Can be Debug, Info, Error")]
        public LogLevels LogLevel { get; set; } = LogLevels.Info;
    }
}
