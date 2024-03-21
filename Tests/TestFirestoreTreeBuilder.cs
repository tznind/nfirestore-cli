using NUnit.Framework;
using FluentAssertions;
using nfirestore_cli;
using Google.Cloud.Firestore;
using Google.Api.Gax;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json.Linq;
using static Grpc.Core.Metadata;

namespace Tests
{
    [FirestoreData]
    public class ExampleObject
    {
        [FirestoreDocumentId]
        public string DocumentId { get; set; }

        [FirestoreProperty]
        public string Property { get; set; }
    }

    [Category("Integration")]
    public class TestFirestoreTreeBuilder
    {
        private FirestoreDb db;        
        private List<DocumentReference> cleanup = new List<DocumentReference>();

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
            cleanup.Add(doc);
        }

        [Test]
        public void TestTreeBuilder_RespectsLimits()
        {
            // Create 4 documents
            for (int i = 0; i < 4; i++)
            {
                var doc = TestDataCreator.CreateTestDocument(db);
                cleanup.Add(doc);
            }

            var rootCollections = db.ListRootCollectionsAsync().ToListAsync().Result;
            rootCollections.Count.Should().Be(1);

            // Limit to 2 results fetched
            var treeBuilder = new FirestoreTreeBuilder(db, 2);
            var children = treeBuilder.GetChildren(rootCollections[0]).ToList();
            children.Count.Should().Be(2);

        }



        [Test]
        public void TestNesting_RootBranchLeaf()
        {
            CollectionReference collection = db.Collection("tenant_1/brands/1");

            var eo = new ExampleObject() { DocumentId = "flibble", Property = "yar" };
            var doc = collection.Document(eo.DocumentId);
            doc.SetAsync(eo, null,CancellationToken.None).Wait();

            cleanup.Add(doc);

            var rootCollections = db.ListRootCollectionsAsync().ToListAsync().Result;

            rootCollections.Count.Should().Be(1);
            rootCollections[0].Id.Should().Be("tenant_1");

            var treeBuilder = new FirestoreTreeBuilder(db, 10);
            var rootsChildren = treeBuilder.GetChildren(rootCollections[0]).ToArray();

            rootsChildren.Length.Should().Be(1);
            FirestoreTreePresenter.AspectGetter(rootsChildren[0])
                .Should().Be("brands");

            var branchChildren = treeBuilder.GetChildren(rootsChildren[0]).ToArray();

            branchChildren.Length.Should().Be(1);
            FirestoreTreePresenter.AspectGetter(branchChildren[0])
                .Should().Be("1");

            var leaves = treeBuilder.GetChildren(branchChildren[0]).ToArray();

            leaves.Length.Should().Be(1);
            FirestoreTreePresenter.AspectGetter(leaves[0])
                .Should().Be("flibble");
        }

        [TearDown]
        public void TearDown()
        {
            foreach(var doc in cleanup)
            {
                doc.DeleteAsync().Wait();
            }
            cleanup.Clear();
        }

    }
}
