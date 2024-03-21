using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.IO.Abstractions;
using System.Reflection;
using Terminal.Gui;

namespace nfirestore_cli
{
    internal class FirestoreTreeBuilder : ITreeBuilder<object>
    {
        private FirestoreDb db;
        private readonly int limit;

        private readonly PropertyInfo pParentPath;
        private readonly PropertyInfo pDocumentsPath;
        private readonly MethodInfo mGetDocumentReferenceFromResourceName;

        public FirestoreTreeBuilder(FirestoreDb db, int limit)
        {
            this.db = db;
            this.limit = limit;
            this.pParentPath = typeof(CollectionReference).GetProperty("ParentPath", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new Exception("Expected property was not present");
            
            this.pDocumentsPath = typeof(FirestoreDb).GetProperty("DocumentsPath", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new Exception("Expected property was not present");

            this.mGetDocumentReferenceFromResourceName = typeof(FirestoreDb).GetMethod("GetDocumentReferenceFromResourceName", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new Exception("Expected method was not present");

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
                    DocumentReference[] toReturn =  ListDocuments(cr).ToArray();
                    return toReturn;
                }

                if (forObject is DocumentReference d)
                {
                    var toReturn = ListCollections(d);
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                return new[] { "Error:" + ex.Message };
            }
            return Enumerable.Empty<object>();
        }

        private IEnumerable<CollectionReference> ListCollections(DocumentReference d)
        {
            var rootDocumentsPath = (string)pDocumentsPath.GetValue(db);
            return db.Client
                .ListCollectionIds(d?.Path ?? rootDocumentsPath, null,limit)
                .Select(id =>db.Collection(id));
        }

        IEnumerable<DocumentReference> ListDocuments(CollectionReference cr)
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

            return db.Client
                .ListDocuments(options)
                .Select(doc =>
                    (DocumentReference)mGetDocumentReferenceFromResourceName.Invoke(db, new[] { doc.Name }));
        }
    }
}