using LanguageTool.WordAddin.Models;
using LanguageTool.WordAddin.Properties;
using LanguageTool.WordAddin.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async static Task<bool> IsTokenValid(string userToken)
        {
            try
            {
                HttpResponseMessage response = await client.
                       GetAsync($"{Settings.Default.tokenValidityEndpoint}{userToken}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(json);
                    if (jObject["result"] == null)
                        return false;
                    else
                    {
                        var res = jObject.GetValue("result").ToObject<bool>();
                        Settings.Default.isTokenValid = res;
                        return res;

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error("Exception while checking if token is valid",
                   ex);
                Settings.Default.isTokenValid = false;
                return false;
            }
        }
        public async static Task<bool> DoesUpdateExist(string userToken)
        {
            try
            {
                HttpResponseMessage response = await client.
                    GetAsync($"{Settings.Default.checkForUpdatesEndpoint}{userToken}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(json);
                    if (  jObject["update_available"] == null)
                        return false;
                    else
                    {
                       var res = jObject.GetValue("update_available").ToObject<bool>();
                        return res;
                    }
                }
                Globals.ThisAddIn.AppLogger.Error(
                    $"Server returned with error {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error("Exception while checking if update exist",
                    ex);
                return false;
               // throw;
            }
        }

        public async static Task<bool> GetUpdatedVersion(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                Globals.ThisAddIn.AppLogger.Info
                    ("token is empty , not getting latest from server");
                return false;
            }
            string updatedJson =  await GetTemplatesFromServer(token);
            if (String.IsNullOrWhiteSpace(updatedJson))
            {
                Globals.ThisAddIn.AppLogger.Info
                    ("Empty Json returned, not updating the file");
                return false;
            }
            if (UpdatedJsonIsValid(updatedJson))
            {
               if( LocalStorageManager.SaveDataToFile(updatedJson,
                      Settings.Default.localSnippetsFileName))
                {
                    return true;
                }
            }
            return false;
        }
        #region utilityMethods
        private async static Task<string> GetTemplatesFromServer(string userToken)
        {
            try
            {
                HttpResponseMessage response = await client.
                    GetAsync($"{Settings.Default.snippetsEndpoint}{userToken}");
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
        #endregion
    }
}
