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
        [Option('p', "project", Required = true, HelpText = "Firestore Project to connect to.")]
        public string Project { get; set; }

        [Option('l', "limit", Default = 100, HelpText = "Maximum number of results to return from any query")]
        public int Limit { get; set; } = 100;
    }
}
