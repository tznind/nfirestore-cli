
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace nfirestore_cli {
    using Terminal.Gui;


    public partial class DatabaseSelector {

        public bool Exit { get; private set; }

        public DatabaseSelector(Options options) {
            InitializeComponent();

            if(!string.IsNullOrWhiteSpace(options.Project))
            {
                tfProject.Text = options.Project;
            }
            tfProject.TextChanged += (s, e) => { options.Project = tfProject.Text; };

            if (!string.IsNullOrWhiteSpace(options.Database))
            {
                tfDatabase.Text = options.Database;
            }
            tfDatabase.TextChanged += (s, e) => { options.Database = tfDatabase.Text; };

            if (!string.IsNullOrWhiteSpace(options.EmulatorUrl))
            {
                tfEmulator.Text = options.EmulatorUrl;
            }
            else
            {
                var envValue = Environment.GetEnvironmentVariable(Options.EmulatorEnvVarKey);

                if(!string.IsNullOrWhiteSpace(envValue))
                {
                    tfEmulator.Text = envValue;
                }
            }
            tfEmulator.TextChanged += (s, e) => { options.EmulatorUrl = tfEmulator.Text; };

            btnOk.Clicked += (s, e) =>
            {
                Application.RequestStop();
            };

            btnExit.Clicked += (s, e) =>
            {
                this.Exit = true;
                Application.RequestStop();
            };


            btnTest.Clicked += (s, e) =>
            {
                try
                {
                    var factory = new DatabaseFactory();
                    var db = factory.Create(options);
                    var rootCollectionsCount = db.ListRootCollectionsAsync().ToListAsync().Result.Count;

                    MessageBox.Query("Success", $"Connected successfully and found {rootCollectionsCount} root collections", "Ok");
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Failed", ex.Message, "Ok");
                }
            };
        }

    }
}