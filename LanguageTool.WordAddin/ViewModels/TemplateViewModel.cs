using LanguageTool.WordAddin.Business;
using LanguageTool.WordAddin.Common;
using LanguageTool.WordAddin.Models;
using LanguageTool.WordAddin.Properties;
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
       public TemplateViewModel()
        {
            LoadSampleData();
        }
        public ObservableCollection<SnippetItem> SnippetItems { get; set; } 
            = new ObservableCollection<SnippetItem>();
        void LoadSampleData()
        {
            try
            {
                var json = LocalStorageManager.GetDataFromFile(Settings.Default.localStorageFileName);
                var itemSnippets = JsonConvert.DeserializeObject<Templates>(json);
                foreach (var item in itemSnippets.Snippets)
                {
                    SnippetItems.Add(item);
                }
            }
            catch (Exception ex)
            {

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

        public ICommand InsertCommand => new RelayCommand<SnippetItem>((_) => SelectedText = _.Body);


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
