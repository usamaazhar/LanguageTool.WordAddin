using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using LanguageTool.WordAddin.Views;
using System.Windows.Forms.Integration;
using LanguageTool.WordAddin.ViewModels;
using LanguageTool.WordAddin.Business;
using System.Threading;

namespace LanguageTool.WordAddin
{
    public partial class ProNETTab
    {
        private string m_actionPaneName = "Language Templates";
        private BaseUserControl userControl;
        private Microsoft.Office.Tools.CustomTaskPane customTaskPane;
        private void ProNETTab_Load(object sender, RibbonUIEventArgs e)
        {
            userControl = new BaseUserControl();
            customTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(userControl, m_actionPaneName);

            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            // Create the WPF UserControl.
            TemplateList uc = new TemplateList();
            host.Child = uc;
            userControl.Controls.Add(host);
        }

        private async void CheckUpdates_BTN_Click(object sender, RibbonControlEventArgs e)
        {
            if( await ServerUpdater.DoesUpdateExist())
            {
                CheckUpdates_BTN.Enabled = false;
                Thread.Sleep(4000);
               await ServerUpdater.GetUpdatedVersion();
                CheckUpdates_BTN.Enabled = true;
                return;
            }
        }

        private void ShowLanguageBar_BTN_Click(object sender, RibbonControlEventArgs e)
        {
            if (ShowLanguageBar_BTN.Checked)
            {
                customTaskPane.Visible = true;
            }
            else
                customTaskPane.Visible = false;
        }

        private void AddTextToCurrentPostion(string text)
        {
            Range rng;
            var selection = Globals.ThisAddIn.Application.Selection;
            rng = selection.Range;
            rng.Text = "New Text" + Guid.NewGuid() as string;
        }
        private void PopulateViewModel()
        {
            //var templateVM = new TemplateViewModel() { TemplateName="Usama",TemplateContent ="Tesst"};

        }
    }
}
