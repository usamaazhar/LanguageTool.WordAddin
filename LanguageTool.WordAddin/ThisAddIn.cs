using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using LanguageTool.WordAddin.Business;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using LanguageTool.WordAddin.Properties;

namespace LanguageTool.WordAddin
{
    public partial class ThisAddIn
    {
        //bool initialized = false;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.WindowActivate += Application_WindowActivate;
        }

        private async void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
        {
            this.Application.WindowActivate -= Application_WindowActivate;
            UserInfoForm form = new UserInfoForm();
            form.ShowDialog();

            //Cancel Was Pressed
            if (String.IsNullOrWhiteSpace(Settings.Default.userID))
                return;
            if (await ServerUpdater.DoesUpdateExist())
            {
               await  ServerUpdater.GetUpdatedVersion();
            }
            
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
