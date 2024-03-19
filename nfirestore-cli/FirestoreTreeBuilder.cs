using Google.Cloud.Firestore;
using Terminal.Gui;

namespace nfirestore_cli
{
    internal class FirestoreTreeBuilder : ITreeBuilder<object>
    {
        private FirestoreDb db;

        public FirestoreTreeBuilder(FirestoreDb db)
        {
            this.db = db;
        }

        public bool SupportsCanExpand => false;

        public bool CanExpand(object toExpand)
        {
            return false;
        }

        public IEnumerable<object> GetChildren(object forObject)
        {
            try
            {

                if (forObject is CollectionReference cr)
                {
                    return cr.ListDocumentsAsync().ToArrayAsync().Result;
                }


                if (forObject is DocumentReference dr)
                {
                    return dr.ListCollectionsAsync().ToArrayAsync().Result;
                }
            }
            catch (Exception ex)
            {
                return new[] { "Error:" + ex.Message };
            }
            return Enumerable.Empty<object>();
        }
    }
}