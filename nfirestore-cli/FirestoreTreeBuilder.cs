using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.Reflection;
using Terminal.Gui;

namespace nfirestore_cli
{
    internal class FirestoreTreeBuilder : ITreeBuilder<object>
    {
        private FirestoreDb db;
        private readonly int limit;
        private readonly PropertyInfo pParentPath;

        public FirestoreTreeBuilder(FirestoreDb db, int limit)
        {
            this.db = db;
            this.limit = limit;
            this.pParentPath = typeof(CollectionReference).GetProperty("ParentPath", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new Exception("Expected property was not present");
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
                    Document[] toReturn =  ListDocuments(cr).ToArray();
                    return toReturn;
                }

                if (forObject is Document d)
                {
                    return ListCollections(d.Name );
                }
            }
            catch (Exception ex)
            {
                return new[] { "Error:" + ex.Message };
            }
            return Enumerable.Empty<object>();
        }

        private IEnumerable<CollectionReference> ListCollections(string name)
        {
            ListCollectionIdsRequest options = new ListCollectionIdsRequest
            {
                PageSize = limit,
                Parent = name,
            };

            return db.Client.ListCollectionIds(options).Select(db.Collection).ToArray();
        }

        IEnumerable<Document> ListDocuments(CollectionReference cr)
        {
            // Construct options with specified page size
            ListDocumentsRequest options = new ListDocumentsRequest
            {
                Parent = (string)pParentPath.GetValue(cr),
                CollectionId = cr.Id,
                PageSize = limit,
                ShowMissing = true,
                Mask = new DocumentMask()
            };

            return db.Client.ListDocuments(options);
        }
    }
}