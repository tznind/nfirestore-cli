using CsvHelper;
using CsvHelper.Configuration;
using Google.Cloud.Firestore;
using System.Globalization;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    internal class CollectionTab : FirestoreTab
    {
        private TableFromCollection tableSource;

        public CollectionReference CollectionReference { get; }

        public CollectionTab(CollectionReference cr, IEnumerable<DocumentReference> children)
        {
            this.CollectionReference = cr;
            var view = new TableView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Table = tableSource = new TableFromCollection(cr, children)
            };

            SetTab(cr.Id, view);
        }

        public override bool Is(DocumentReference dr)
        {
            return false;
        }

        public override bool Is(CollectionReference cr)
        {
            return cr == CollectionReference;
        }

        protected override void WriteFileContents(Stream stream)
        {
            tableSource.WriteTo(stream);
        }

        protected override string GetFilename()
        {
            return CollectionReference.Id + ".csv";
        }
    }
}
