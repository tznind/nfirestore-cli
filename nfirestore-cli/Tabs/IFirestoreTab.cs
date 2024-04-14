using Google.Cloud.Firestore;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    internal interface IFirestoreTab
    {
        public Tab Tab { get; }
        public bool Is(DocumentReference dr);
        public bool Is(CollectionReference cr);
        void SaveAs();
    }
}
