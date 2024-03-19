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
    }
}
