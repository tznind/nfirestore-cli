using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    abstract class FirestoreTab : IFirestoreTab
    {
        public Tab Tab { get; private set; }

        public abstract bool Is(DocumentReference dr);

        public abstract bool Is(CollectionReference cr);

        public void SaveAs()
        {
            var sd = new SaveDialog()
            {
                Title = "Save As"
            };

            sd.Path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                GetFilename());

            Application.Run(sd);

            if(!sd.Canceled)
            {
                using (var stream = File.OpenWrite(sd.Path))
                {
                    WriteFileContents(stream);
                }
                    
            }
        }

        protected abstract void WriteFileContents(Stream stream);
        protected abstract string GetFilename();

        protected void SetTab(string name, View view)
        {
            Tab = new Tab()
            {
                Text = GetTabName(name),
                View = view
            };
        }

        private string GetTabName(string name)
        {
            if (name.Length > 8)
            {
                name = name.Substring(0, 6) + "…";
            }

            return "[X]" + name;
        }

    }
}
