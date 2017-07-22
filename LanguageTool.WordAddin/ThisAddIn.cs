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
using log4net;

namespace LanguageTool.WordAddin
{
    public partial class ThisAddIn
    {
        private readonly ILog _log =
        LogManager.GetLogger("RollingFileAppender");
        //bool initialized = false;
        private Dispatcher _dispatcher;

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
            set { _dispatcher = value; }
        }

        public ILog AppLogger
        {
            get
            {
                return _log;
            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
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
