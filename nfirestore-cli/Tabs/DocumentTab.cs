using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    internal class DocumentTab : FirestoreTab
    {
        public DocumentReference DocumentReference { get; }

        private TextView textView;

        public DocumentTab(DocumentSnapshot snap)
        {
            this.DocumentReference = snap.Reference;

            textView = new TextView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                WordWrap = false,
                AllowsReturn = false,
                Multiline = true,
            };

            OpenDocumentIn(textView, snap);

            SetTab(snap.Id, textView);
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

        protected override void WriteFileContents(Stream stream)
        {
            using (var tw = new StreamWriter(stream))
            {
                tw.Write(textView.Text);
            }
        }

        protected override string GetFilename()
        {
            return DocumentReference.Id + ".json";
        }
    }
}
