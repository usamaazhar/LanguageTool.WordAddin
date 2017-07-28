using LanguageTool.WordAddin.Business;
using LanguageTool.WordAddin.Common;
using LanguageTool.WordAddin.Models;
using LanguageTool.WordAddin.Properties;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LanguageTool.WordAddin.ViewModels
{
   
    public class Templates
    {
        public List<SnippetItem> Snippets { get; set; }
    }
    
    public class TemplateViewModel
    {
        public event Action _updateViewModel;
        private static TemplateViewModel _instance = new TemplateViewModel();
        private TemplateViewModel()
        {
            //_updateViewModel += TemplateViewModel__updateViewModel;
            LoadDataFromFile();
        }

        private void TemplateViewModel__updateViewModel()
        {
            UpdateSnippets();
        }

        public static TemplateViewModel GetInstance()
        {
            return _instance;
        }
        public void UpdateSnippets ()
        {
            SnippetItems.Clear();
            LoadDataFromFile();
            NotifyPropertyChange(nameof(SnippetItems));


        }
        public ObservableCollection<SnippetItem> SnippetItems { get; set; } 
            = new ObservableCollection<SnippetItem>();
        void LoadDataFromFile()
        {
            try
            {
                var json = LocalStorageManager.GetDataFromFile(Settings.Default.localSnippetsFileName);
                var itemSnippets = JsonConvert.DeserializeObject<Templates>(json);
                foreach (var item in itemSnippets.Snippets)
                {
                    SnippetItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error
                    ("The Json Saved in file could not be parsed", ex);
            }            
        }


        private SnippetItem selectedSnippet;

        public SnippetItem SelectedSnippet
        {
            get { return selectedSnippet; }
            set
            {
                selectedSnippet = value;
                NotifyPropertyChange(nameof(SelectedSnippet));
            }
        }

        public ICommand InsertCommand => 
            new RelayCommand<SnippetItem>((_) => 
            {
                SelectedText = _.Body;
                InsertTextToCurrentCursor(SelectedText);
            });
        private void InsertTextToCurrentCursor(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return;
            Range rng;
            var selection = Globals.ThisAddIn.Application.Selection;
            rng = selection.Range;
            rng.Text = text;
        }

        private string selectedText;

        public string SelectedText
        {
            get { return selectedText; }
            set
            {
                selectedText = value;
                NotifyPropertyChange(nameof(SelectedText));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}
