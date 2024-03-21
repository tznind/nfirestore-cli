using NUnit.Framework;
using FluentAssertions;
using nfirestore_cli;
using Google.Cloud.Firestore;
using Google.Api.Gax;

namespace Tests
{
    [Category("Integration")]
    public class TestFirestoreTreeBuilder
    {
        private FirestoreDb db;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Environment.SetEnvironmentVariable("FIRESTORE_EMULATOR_HOST", "127.0.0.1:8200");

            var builder = new FirestoreDbBuilder
            {
                ProjectId = "dummy-project-id",
                EmulatorDetection = EmulatorDetection.EmulatorOnly
            };

            db = builder.Build();
        }

        [Test]
        public void TestTreeBuilder()
        {
            var treeBuilder = new FirestoreTreeBuilder(db,10);

            var doc = TestDataCreator.CreateTestDocument(db);
            treeBuilder.GetChildren(doc).Should().BeEmpty();
        }

        [Test]
        public void TestNesting_RootBranchLeaf()
        {
            CollectionReference collection = db.Collection("tenant_1/brands/1");
            collection.AddAsync(new { Test = "yes"}).Wait();

            var rootCollections = db.ListRootCollectionsAsync().ToListAsync().Result;

            rootCollections.Count.Should().Be(1);
            rootCollections[0].Id.Should().Be("tenant_1");

            var treeBuilder = new FirestoreTreeBuilder(db, 10);
            var rootsChildren = treeBuilder.GetChildren(rootCollections[0]).ToArray();

            rootsChildren.Length.Should().Be(1);
            FirestoreTreePresenter.AspectGetter(rootsChildren[0])
                .Should().Be("brands");


        }

        [TearDown]
        public void TearDown()
        {
            foreach(var c in db.ListRootCollectionsAsync().ToListAsync().Result)
            {
                DeleteCollection(c).Wait();
            }
        }

        private static async Task DeleteCollection(CollectionReference collectionReference)
        {
            QuerySnapshot snapshot = await collectionReference.GetSnapshotAsync();
            IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
            while (documents.Count > 0)
            {
                foreach (DocumentSnapshot document in documents)
                {
                    TestContext.Out.WriteLine("Deleting document {0}", document.Id);
                    await document.Reference.DeleteAsync();
                }
                snapshot = await collectionReference.GetSnapshotAsync();
                documents = snapshot.Documents;
            }

            TestContext.Out.WriteLine("Finished deleting all documents from the collection.");
        }
    }
}
