using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace nfirestore_cli
{
    internal class DatabaseFactory
    {
        public FirestoreDb Create(Options options)
        {
            var emulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOrProduction;

            if (!string.IsNullOrWhiteSpace(options.EmulatorUrl))
            {
                Environment.SetEnvironmentVariable(Options.EmulatorEnvVarKey, options.EmulatorUrl);
                emulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOnly;
            }
            else
            {
                Environment.SetEnvironmentVariable(Options.EmulatorEnvVarKey, null);

            }

            var builder = new FirestoreDbBuilder
            {
                ProjectId = options.Project,
                EmulatorDetection = emulatorDetection
            };

            if(!string.IsNullOrWhiteSpace(options.Database))
            {
                builder.DatabaseId = options.Database;
            }

            return builder.Build();
        }
    }
}
