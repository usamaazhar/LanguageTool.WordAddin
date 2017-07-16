﻿using Newtonsoft.Json;
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
                    //var serializer = JsonSerializer.Create();
                    //serializer.Serialize(writer, data);
                    writer.Write(json);
      
                    return true;
                }
                
            }
            catch (Exception ee)
            {
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
            catch (Exception ee)
            {
                return string.Empty ;
            }
        }

    }
}