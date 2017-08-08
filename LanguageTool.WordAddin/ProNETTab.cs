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
using MVVMLight.Messaging;
using LanguageTool.WordAddin.Models;

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

        private void ShowInfoMessageBox(string message, string title)
        {
            MessageBox.Show(message, title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void CheckUpdates_BTN_Click(object sender, RibbonControlEventArgs e)
        {
            CheckUpdates_BTN.Enabled = false;
            var snippetsUpdated = await RunFetchWorkflowForUpdatesBTN();
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

        private async System.Threading.Tasks.Task<bool> CheckTokenValidity()
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
            {
                ShowInfoMessageBox("Token is still invalid", "Invalid Token");
                return false;
            }
            return true;
        }
        private async System.Threading.Tasks.Task<bool> RunFetchWorkflow()
        {
            if (!await CheckTokenValidity()) //token is still invalid after all tries
                return false;
            var updatedToken = LocalStorageManager.GetUserToken();
            if (await ServerUpdater.DoesUpdateExist(updatedToken))
            {
                await GetUpdatedJsonAndUpdateVM(updatedToken);
            }
            else
            {
                if (!LocalStorageManager.DoesFileExistWithJson
                    (Settings.Default.localSnippetsFileName))
                {
                    await GetUpdatedJsonAndUpdateVM(updatedToken);
                }
            }

            return false;
        }

        private async System.Threading.Tasks.Task<bool> RunFetchWorkflowForUpdatesBTN()
        {
            if (!await CheckTokenValidity()) //token is still invalid after all tries
                return false;
            var updatedToken = LocalStorageManager.GetUserToken();
            if (await ServerUpdater.DoesUpdateExist(updatedToken))
            {
                if (await GetUpdatedJsonAndUpdateVM(updatedToken))
                    ShowInfoMessageBox("Updates were available and were fetched", "Fetch success");
                else
                    ShowInfoMessageBox("Updates were available but newly fetched templated were empty",
                        "Empty templates returned");
            }
            else
            {
                if (!LocalStorageManager.DoesFileExistWithJson
                    (Settings.Default.localSnippetsFileName))
                {
                    if (await GetUpdatedJsonAndUpdateVM(updatedToken))
                        ShowInfoMessageBox("Updates were available and were fetched", "Fetch success");
                    else
                        ShowInfoMessageBox("Updates were available but newly fetched templated were empty",
                            "Empty templates returned");
                    return true;
                }

                ShowInfoMessageBox("No updates were available", "Not fetched");
            }

            return true;
        }
        private async System.Threading.Tasks.Task<bool> GetUpdatedJsonAndUpdateVM(string token)
        {
            if (await ServerUpdater.GetUpdatedVersion(token))
            {
                await Globals.ThisAddIn.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                  new System.Action(() => Messenger.Default.Send(new UpdateSnippetsMessage())));
                return true;
            }
            return false;
        }

    }
}
