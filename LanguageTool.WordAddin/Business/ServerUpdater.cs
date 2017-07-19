using LanguageTool.WordAddin.Models;
using LanguageTool.WordAddin.Properties;
using LanguageTool.WordAddin.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
namespace LanguageTool.WordAddin.Business
{
   public class ServerUpdater
    {
        
        static HttpClient client = new HttpClient();

        static ServerUpdater()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
        }
        public async static Task<bool> DoesUpdateExist()
        {
            await Task.Delay(1000);
            return true;
        }

        public async static Task<bool> GetUpdatedVersion()
        {
            var userID = Settings.Default.userID;
            if (String.IsNullOrWhiteSpace(userID))
            {
                Globals.ThisAddIn.AppLogger.Info
                    ("UserID is empty , not getting latest from server");
                return false;
            }
            string updatedJson =  await GetTemplatesFromServer(userID);
            if (String.IsNullOrWhiteSpace(updatedJson))
            {
                Globals.ThisAddIn.AppLogger.Info
                    ("Empty Json returned, not updating the file");
                return false;
            }
            if (UpdatedJsonIsValid(updatedJson))
            {
               if( LocalStorageManager.SaveDataToFile(updatedJson,
                      Settings.Default.localStorageFileName))
                {
                    return true;
                }
            }
            return false;
        }
        public async static Task<string> GetTemplatesFromServer(string userID)
        {
            try
            {
                HttpResponseMessage response = await client.
                    GetAsync($"{Settings.Default.serverBaseURL}{userID}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                Globals.ThisAddIn.AppLogger.Error(
                    $"Server returned with error {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error
                   ("Exception in Getting snippets from server", ex);
            }
            return string.Empty;
        }
        private static bool UpdatedJsonIsValid(string updatedJson)
        {
            try
            {
                JsonConvert.DeserializeObject<SnippetItem>(updatedJson);
                return true;
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error
                   ("Exception Json Validity", ex);
                return false;
            }
        }
    }
}
