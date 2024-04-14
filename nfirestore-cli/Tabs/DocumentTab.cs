using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    internal class DocumentTab : FirestoreTab
    {
        public DocumentReference DocumentReference { get; }

        public DocumentTab(DocumentSnapshot snap)
        {
            this.DocumentReference = snap.Reference;

            var view = new TextView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                WordWrap = false,
                AllowsReturn = false,
                Multiline = true,
            };

            OpenDocumentIn(view, snap);

            SetTab(snap.Id, view);
        }

        internal static void OpenDocumentIn(TextView currentDocumentTextView, DocumentSnapshot snap)
        {
            currentDocumentTextView.Text = JsonConvert.SerializeObject(snap.ToDictionary(), Formatting.Indented);
        }

        public override bool Is(DocumentReference dr)
        {
            return dr == DocumentReference;
        }

        public override bool Is(CollectionReference cr)
        {
            return false;
        }
    }
}
