using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageTool.WordAddin.Models
{
    public class SnippetItem : INotifyPropertyChanged
    {
        private string name;

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChange(nameof(Name));
            }
        }

        private string body;

        [JsonProperty("body")]
        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                NotifyPropertyChange(nameof(Body));
            }
        }
        [JsonIgnore]

        private bool isExpanded;

        [JsonIgnore]
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                NotifyPropertyChange(nameof(IsExpanded));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
