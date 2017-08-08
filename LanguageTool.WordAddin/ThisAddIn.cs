using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using LanguageTool.WordAddin.Business;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using LanguageTool.WordAddin.Properties;
using LanguageTool.WordAddin.ViewModels;
using log4net;
using LanguageTool.WordAddin.Models;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms.Integration;
using LanguageTool.WordAddin.Views;

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


        public static readonly string DocumentCustomPropertyName = "LanguageToolPluginWbUniqueIdentifier";

        class WorkbookCustomPropertyComparer : IEqualityComparer<Document>
        {
            public bool Equals(Document x, Document y)
            {
                try
                {
                    var prop1 = ((dynamic)x.CustomDocumentProperties)[DocumentCustomPropertyName].Value.ToString();
                    var prop2 = ((dynamic)y.CustomDocumentProperties)[DocumentCustomPropertyName].Value.ToString();

                    return (prop1 == prop2);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public int GetHashCode(Document obj)
            {
                try
                {
                    var prop1 = ((dynamic)obj.CustomDocumentProperties)[DocumentCustomPropertyName].Value.ToString();
                    return prop1.GetHashCode();
                }
                catch (Exception ex)
                {
                    return "".GetHashCode();
                }
            }
        }

        private Dictionary<Document, MappingData> _documentMappings = new Dictionary<Document, MappingData>();

        public Dictionary<Document, MappingData> DocumentMappings
        {
            get { return _documentMappings; }
            set { _documentMappings = value; }
        }

        private void CheckAndRemoveCustomProperty(Document Doc)
        {
            Microsoft.Office.Core.DocumentProperties properties;
            properties = (Microsoft.Office.Core.DocumentProperties)Doc.CustomDocumentProperties;

            if (ReadDocumentProperty(Doc, DocumentCustomPropertyName) != null)
            {
                properties[DocumentCustomPropertyName].Delete();
            }
        }

        public string ReadDocumentProperty(Document Doc, string propertyName)
        {
            Office.DocumentProperties properties;
            properties = (Office.DocumentProperties)Doc.CustomDocumentProperties;

            foreach (Office.DocumentProperty prop in properties)
            {
                if (prop.Name == propertyName)
                {
                    return prop.Value.ToString();
                }
            }
            return null;
        }

        private string _actionPaneTitle = "Language Templates";

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

            Application.WindowActivate += Application_WindowActivate;
            Application.DocumentBeforeClose += Application_DocumentBeforeClose;
        }

        private void Application_WindowActivate(Document Doc, Window Wn)
        {
            if (!DocumentMappings.ContainsKey(Doc))
            {
                CheckAndRemoveCustomProperty(Doc);

                ((dynamic)Doc.CustomDocumentProperties).
                    Add(DocumentCustomPropertyName, false, Microsoft.Office.Core.MsoDocProperties.msoPropertyTypeString, Guid.NewGuid().ToString());

                var userControl = new BaseUserControl();
                var customTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(userControl, _actionPaneTitle);
                ElementHost host = new ElementHost();
                host.Dock = System.Windows.Forms.DockStyle.Fill;

                // Create the WPF UserControl.
                TemplateList uc = new TemplateList();
                host.Child = uc;
                userControl.Controls.Add(host);


                DocumentMappings.Add(Doc, new MappingData { TaskPane = customTaskPane });
            }
        }

        private void Application_DocumentBeforeClose(Document Doc, ref bool Cancel)
        {
            if (DocumentMappings.ContainsKey(Doc))
            {
                DocumentMappings.Remove(Doc);
                CheckAndRemoveCustomProperty(Doc);
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();
        }


        public Microsoft.Office.Tools.CustomTaskPane GetCurrentTaskPane()
        {
            var Doc = Application.ActiveDocument;

            if (DocumentMappings.ContainsKey(Doc))
            {
                var keyValuePair = DocumentMappings[Doc];
                return keyValuePair.TaskPane;
            }

            return null;
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
