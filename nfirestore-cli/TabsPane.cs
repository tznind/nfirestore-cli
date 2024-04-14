
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace nfirestore_cli {
    using Google.Cloud.Firestore;
    using Newtonsoft.Json;
    using System;
    using Terminal.Gui;
    
    
    public partial class TabsPane {
        
        public TabsPane() {
            InitializeComponent();
        }

        internal void OpenDocument(DocumentSnapshot snap, bool newTab)
        {
            if(newTab)
            {
                var name = GetTabName(snap);
                var view = new TextView
                {
                    Width = Dim.Fill(),
                    Height = Dim.Fill(),
                    WordWrap = false,
                    AllowsReturn = false,
                    Multiline = true,
                };

                var tab = new Tab()
                {
                    Text = name,
                    View = view
                };

                tabView.AddTab(tab, true);
                OpenDocumentIn(view, snap);
            }
            else
            {
                OpenDocumentIn(currentDocumentTextView, snap);
            }
        }

        private string GetTabName(DocumentSnapshot snap)
        {
            string name = snap.Reference.Id;
            if(name.Length > 8)
            {
                name = name.Substring(0, 6) + "�";
            }

            return "[X] " + name;
        }

        private void OpenDocumentIn(TextView currentDocumentTextView, DocumentSnapshot snap)
        {
            currentDocumentTextView.Text = JsonConvert.SerializeObject(snap.ToDictionary(), Formatting.Indented);
        }
    }
}