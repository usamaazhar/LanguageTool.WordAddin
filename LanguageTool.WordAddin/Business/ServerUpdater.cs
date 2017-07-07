using LanguageTool.WordAddin.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LanguageTool.WordAddin.Business
{
   public class ServerUpdater
    {
        private static string updateURL = "";

        public async static Task<bool> DoesUpdateExist()
        {
            await Task.Delay(1000);
            return true;
        }

        public async static Task GetUpdatedVersion()
        {
            string updatedJson = await GetTemplatesFromServer();
            if (UpdatedJsonIsValid(updatedJson))
                LocalStorageManager.SaveDataToFile(updatedJson,
                    Settings.Default.localStorageFileName);
        }
        public async static Task<string> GetTemplatesFromServer()
        {
            await Task.Delay(1000);
            return System.IO.File.ReadAllText(@"C:/test.json");
        }
        private static bool UpdatedJsonIsValid(string updatedJson)
        {
            return true;
           // throw new NotImplementedException();
        }
    }
}
