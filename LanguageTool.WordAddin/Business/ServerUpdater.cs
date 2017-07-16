using LanguageTool.WordAddin.Properties;
using LanguageTool.WordAddin.ViewModels;
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
        public static bool DoesUpdateExistSync()
        {
            //  await Task.Delay(1000);
            bool isUpdateAvailable = Task.Run(() => QueryServerForUpdate()).Result;
            return isUpdateAvailable;
        }

        private async static Task<bool> QueryServerForUpdate()
        {
            await Task.Delay(1000);
            return true;
        }

        public async static Task GetUpdatedVersion()
        {
            var userID = Settings.Default.userID;
            if (String.IsNullOrWhiteSpace(userID))
                return;
            string updatedJson =  await GetTemplatesFromServer(userID);
            if (UpdatedJsonIsValid(updatedJson))
            {
               if( LocalStorageManager.SaveDataToFile(updatedJson,
                      Settings.Default.localStorageFileName))
                {
                    var vm = TemplateViewModel.GetInstance();

                   await Dispatcher.CurrentDispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                      new Action(() => vm.UpdateSnippets()));
                }
            }
        }
        public async static Task<string> GetTemplatesFromServer(string userID)
        {

            HttpResponseMessage response = await client.
                GetAsync($"{Settings.Default.serverBaseURL}{userID}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return string.Empty;
           // return System.IO.File.ReadAllText(@"C:/test.json");
        }
        private static bool UpdatedJsonIsValid(string updatedJson)
        {
            return true;
           // throw new NotImplementedException();
        }
    }
}
