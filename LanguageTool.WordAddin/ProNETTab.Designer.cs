﻿namespace LanguageTool.WordAddin
{
    partial class ProNETTab : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ProNETTab()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pronet_tab = this.Factory.CreateRibbonTab();
            this.Language_bar = this.Factory.CreateRibbonGroup();
            this.CheckUpdates_BTN = this.Factory.CreateRibbonButton();
            this.toggleLanguageBarButton = this.Factory.CreateRibbonButton();
            this.Pronet_tab.SuspendLayout();
            this.Language_bar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pronet_tab
            // 
            this.Pronet_tab.Groups.Add(this.Language_bar);
            this.Pronet_tab.Label = "ProNet";
            this.Pronet_tab.Name = "Pronet_tab";
            // 
            // Language_bar
            // 
            this.Language_bar.Items.Add(this.toggleLanguageBarButton);
            this.Language_bar.Items.Add(this.CheckUpdates_BTN);
            this.Language_bar.Label = "Language Bar";
            this.Language_bar.Name = "Language_bar";
            // 
            // CheckUpdates_BTN
            // 
            this.CheckUpdates_BTN.Label = "Check For Updates";
            this.CheckUpdates_BTN.Name = "CheckUpdates_BTN";
            this.CheckUpdates_BTN.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CheckUpdates_BTN_Click);
            // 
            // toggleLanguageBarButton
            // 
            this.toggleLanguageBarButton.Label = "Toggle Language Bar";
            this.toggleLanguageBarButton.Name = "toggleLanguageBarButton";
            this.toggleLanguageBarButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleLanguageBarButton_Click);
            // 
            // ProNETTab
            // 
            this.Name = "ProNETTab";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.Pronet_tab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ProNETTab_Load);
            this.Pronet_tab.ResumeLayout(false);
            this.Pronet_tab.PerformLayout();
            this.Language_bar.ResumeLayout(false);
            this.Language_bar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab Pronet_tab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Language_bar;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CheckUpdates_BTN;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton toggleLanguageBarButton;
    }

    partial class ThisRibbonCollection
    {
        internal ProNETTab ProNETTab
        {
            get { return this.GetRibbon<ProNETTab>(); }
        }
    }
}
