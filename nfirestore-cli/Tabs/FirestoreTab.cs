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
