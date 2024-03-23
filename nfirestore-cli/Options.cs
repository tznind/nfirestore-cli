using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfirestore_cli
{
    public class Options
    {
        /// <summary>
        /// The name of the environment variable that dotnet picks up on to detect
        /// that it should connect to Firestore emulator instead of GCP.
        /// </summary>
        public const string EmulatorEnvVarKey = "FIRESTORE_EMULATOR_HOST";


        [Option('p', "project", Required = false, HelpText = "Firestore Project to connect to.")]
        public string Project { get; set; }

        [Option('d', "Database", Required = false, HelpText = "Firestore database (leave blank for <default>).")]
        public string Database { get; set; }

        [Option('l', "limit", Default = 100, HelpText = "Maximum number of results to return from any query")]
        public int Limit { get; set; } = 100;

        [Option('e', "Emulator", Required = false, HelpText = "Sets FIRESTORE_EMULATOR_HOST environment variable for the application lifetime.")]
        public string EmulatorUrl { get; set; }

        internal bool IsFullyPopulated()
        {
            return !string.IsNullOrEmpty(Project);
        }
    }
}
