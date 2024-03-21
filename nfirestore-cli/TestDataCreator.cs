using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfirestore_cli
{
    public class TestDataCreator
    {
        public static DocumentReference CreateNestedDocument(FirestoreDb db)
        {
            CollectionReference collection = db.Collection("test_nested/level1/level2");
            var r = new Random();

            return collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = r.Next(1900, 2024) }).Result;
        }

        public static DocumentReference CreateTestDocument(FirestoreDb db)
        {
            CollectionReference collection = db.Collection("test_collection");

            var r = new Random();

            return collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = r.Next(1900, 2024) }).Result;
        }
    }
}
