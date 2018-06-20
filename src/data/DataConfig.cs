using System;
using System.Collections.Generic;
using System.Data.SQLite;
using IRSACA.src;

namespace IRSACA {
    public class DataConfig {

     static string path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\netcoreapp2.1", "\\");
     public static string aca_conn = path + "src\\data\\aca.db"; 

      public static string GetDataConnection(string path){
           return DataManager.GetConnection(path);
      } 

    }
}