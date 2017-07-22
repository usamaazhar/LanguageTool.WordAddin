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
using System.Threading.Tasks;
using System.Windows.Threading;
using LanguageTool.WordAddin.Properties;

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
            CheckUpdates_BTN.Enabled = false;
            var snippetsUpdated = await RunFetchWorkflow();
            CheckUpdates_BTN.Enabled = true;
            return;
        }

        private async void ShowLanguageBar_BTN_Click(object sender, RibbonControlEventArgs e)
        {
            if (ShowLanguageBar_BTN.Checked)
            {
                var snippetsUpdated = await RunFetchWorkflow();
                customTaskPane.Visible = true;
            }
            else
                customTaskPane.Visible = false;
        }
        private async System.Threading.Tasks.Task<bool> RunFetchWorkflow()
        {


            var userToken = LocalStorageManager.GetUserToken();//get token from local storagee
            if (Settings.Default.retriesLeft <= 0)
            {
                MessageBox.Show("You have used maximum retries to enter a valid token, restart word to continue",
                      "Max limit reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            Globals.ThisAddIn.AppLogger.Info("checking token validity");
            if (!await ServerUpdater.IsTokenValid(userToken))
            {
                Globals.ThisAddIn.AppLogger.Info(" token not valid  ");
                //if exsisting token is not valid show user the form to enter new
                UserInfoForm form = new UserInfoForm();
                var result = form.ShowDialog();
            }
            if (!Settings.Default.isTokenValid)// checks if token is still invalid after promoting the dialog
                return false;
            var updatedToken = LocalStorageManager.GetUserToken();
            if (await ServerUpdater.DoesUpdateExist(updatedToken))
            {

                if (await ServerUpdater.GetUpdatedVersion(updatedToken))
                {
                    var vm = TemplateViewModel.GetInstance();
                    await Globals.ThisAddIn.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                      new System.Action(() => vm.UpdateSnippets()));
                    return true;
                }
            }

            return false;
        }

    }
}
