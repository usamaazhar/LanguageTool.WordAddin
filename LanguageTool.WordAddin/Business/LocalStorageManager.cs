using LanguageTool.WordAddin.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageTool.WordAddin.Business
{
    public class LocalStorageManager
    {

        public static bool SaveDataToFile(string json, string filename, int @try = 1)
        {
           try
            {
                using (var stream = new IsolatedStorageFileStream(filename, FileMode.Create, GetStore()))
                using (var writer = new StreamWriter(stream))
                {                 
                    writer.Write(json);     
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error("Exception while saving data to file",
                   ex);
                return false;
            }
        }
        static IsolatedStorageFile GetStore() => IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        public static string GetDataFromFile(string filename) 
        {
            try
            {
                using (var stream = new IsolatedStorageFileStream(filename, FileMode.Open, GetStore()))
                using (var reader = new StreamReader(stream))
                {
                    var json =  reader.ReadToEnd();
                    return json;
                }
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.AppLogger.Error("Exception while loading data from file into pane",
                    ex);
                return string.Empty ;
            }
        }

        public static string GetUserToken()
        {
            var token = GetDataFromFile(Settings.Default.localTokenFileName);
            return token;
        
        }

        public static bool UpdateUserToken(string token)
        {
            var isUpdateSuccess = 
                SaveDataToFile(token, Settings.Default.localTokenFileName);
            return isUpdateSuccess;
        }
    }
}
