
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace nfirestore_cli {
    using Google.Cloud.Firestore;
    using Google.Cloud.Firestore.V1;
    using Newtonsoft.Json;
    using Terminal.Gui;
    
    
    public partial class MainWindow {
        private readonly Options options;
        private FirestoreDb db;

        public MainWindow(Options o) {
            InitializeComponent();

            var tiles = new TileView(2);


            this.options = o;
            createTestDocumentsMenuItem.Action = CreateTestDocuments;
            createTestNestedDocumentsMenuItem.Action = CreateNestedTestDocuments;
            textField.KeyDown += TextField_KeyDown;
            exitMenuItem.Action = ()=>Application.RequestStop();
        }

        private void TextField_KeyDown(object sender, Key e)
        {
            if(e.KeyCode == Key.Enter)
            {
                ShowDocument(textField.Text);
            }
        }

        private void ShowDocument(string text)
        {
            if(db == null || string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            try
            {
                ShowDocument(db.Document(text));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void ShowDocument(DocumentReference dr)
        {
            try
            {
                var snap = dr.GetSnapshotAsync().Result;
                frameViewData.Title = "Data - " + dr.Id;
                textViewData.Text = JsonConvert.SerializeObject(snap.ToDictionary(), Formatting.Indented);
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void CreateNestedTestDocuments()
        {
            if (db == null)
            {
                return;
            }
            try
            {
                TestDataCreator.CreateNestedDocument(db);
                RefreshTree();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void CreateTestDocuments()
        {
            if(db == null)
            {
                return;
            }
            try
            {
                TestDataCreator.CreateTestDocument(db);
                RefreshTree();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void RefreshTree()
        {
            if(db == null)
            {
                return;
            }
            try
            {
                tlvObjects.ClearObjects();
                var collections = db.ListRootCollectionsAsync().ToArrayAsync().Result;
                if(collections.Length > 0)
                {
                    tlvObjects.AddObjects(collections);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void ShowException(Exception ex)
        {
            MessageBox.ErrorQuery("Error", ex.Message, "Close");
        }

        public override void OnLoaded()
        {
            base.OnLoaded();

            var emu = Environment.GetEnvironmentVariable("FIRESTORE_EMULATOR_HOST");

            try
            {
                var builder = new FirestoreDbBuilder
                {
                    ProjectId = options.Project,
                    EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOrProduction
                };

                this.db = builder.Build();

                tlvObjects.TreeBuilder = new FirestoreTreeBuilder(this.db, this.options.Limit);
                tlvObjects.AspectGetter = FirestoreTreePresenter.AspectGetter;
                tlvObjects.SelectionChanged += TlvObjects_SelectionChanged;
                
                RefreshTree();
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Error Loading", $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Emulator Variable is:{(string.IsNullOrWhiteSpace(emu) ? "missing":emu)}", "Close");
            }
        }

        private void TlvObjects_SelectionChanged(object sender, SelectionChangedEventArgs<object> e)
        {
            if(tlvObjects.SelectedObject is DocumentReference dr)
            {
                ShowDocument(dr);
            }
        }
    }
}
