
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace nfirestore_cli {
    using System;
    using Terminal.Gui;
    using System.Collections;
    using System.Collections.Generic;
    
    
    public partial class NavigationPane : Terminal.Gui.View {
        
        private Terminal.Gui.TreeView<object> treeView1;
        
        private Terminal.Gui.Label label;
        
        private Terminal.Gui.TextField tfLookup;
        
        private void InitializeComponent() {
            this.tfLookup = new Terminal.Gui.TextField();
            this.label = new Terminal.Gui.Label();
            this.treeView1 = new Terminal.Gui.TreeView<object>();
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Visible = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.treeView1.Width = Dim.Fill(0);
            this.treeView1.Height = Dim.Fill(2);
            this.treeView1.X = 0;
            this.treeView1.Y = 0;
            this.treeView1.Visible = true;
            this.treeView1.Data = "treeView1";
            this.treeView1.Text = "";
            this.treeView1.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.treeView1.Style.CollapseableSymbol = new System.Text.Rune('-');
            this.treeView1.Style.ColorExpandSymbol = false;
            this.treeView1.Style.ExpandableSymbol = new System.Text.Rune('+');
            this.treeView1.Style.InvertExpandSymbolColors = false;
            this.treeView1.Style.LeaveLastRow = false;
            this.treeView1.Style.ShowBranchLines = true;
            this.Add(this.treeView1);
            this.label.Width = 4;
            this.label.Height = 1;
            this.label.X = 0;
            this.label.Y = Pos.AnchorEnd(2);
            this.label.Visible = true;
            this.label.Data = "label";
            this.label.Text = "Document Lookup:";
            this.label.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label);
            this.tfLookup.Width = Dim.Fill(0);
            this.tfLookup.Height = 1;
            this.tfLookup.X = 0;
            this.tfLookup.Y = Pos.AnchorEnd(1);
            this.tfLookup.Visible = true;
            this.tfLookup.Secret = false;
            this.tfLookup.Data = "tfLookup";
            this.tfLookup.Text = "";
            this.tfLookup.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.tfLookup);
        }
    }
}