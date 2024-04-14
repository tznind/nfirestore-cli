using Google.Cloud.Firestore;
using Terminal.Gui;

namespace nfirestore_cli.Tabs
{
    internal class CollectionTab : FirestoreTab
    {
        public CollectionReference CollectionReference { get; }

        public CollectionTab(CollectionReference cr, IEnumerable<DocumentReference> children)
        {
            this.CollectionReference = cr;
            var view = new TableView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Table = new TableFromCollection(cr, children)
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
    }
}
