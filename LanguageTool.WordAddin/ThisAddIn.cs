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

namespace LanguageTool.WordAddin
{
    public partial class ThisAddIn
    {
        //bool initialized = false;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
           
        }

        private async void Application_DocumentOpen(Word.Document Doc)
        {
            if (await ServerUpdater.DoesUpdateExist())
            {
                await ServerUpdater.GetUpdatedVersion();
            }
        }

        private void InitializeCustom()
        {
           // initialized = true;
            Globals.ThisAddIn.Application.Startup += Application_Startup;
          //  Globals.ThisAddIn.Application.DocumentOpen += Application_DocumentOpen;
        }

        private  async void Application_Startup()
        {            
        if (await ServerUpdater.DoesUpdateExist())
        {
            await ServerUpdater.GetUpdatedVersion();
        } 
            
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Globals.ThisAddIn.Application.Startup -= Application_Startup;
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
