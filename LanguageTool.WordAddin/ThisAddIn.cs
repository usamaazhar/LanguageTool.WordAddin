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
using LanguageTool.WordAddin.ViewModels;

namespace LanguageTool.WordAddin
{
    public partial class ThisAddIn
    {
        //bool initialized = false;
        private Dispatcher _dispatcher;

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
            set { _dispatcher = value; }
        }


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
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
              if( await ServerUpdater.GetUpdatedVersion())
                {
                    var vm = TemplateViewModel.GetInstance();
                    await Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                      new System.Action(() => vm.UpdateSnippets()));
                }
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
