using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Fluent;

namespace RadioInformationCenter.Tools
{
   public static class Helper
    {
        public enum ModuleWorkTypeEnum
        {
            NormalWork = 0,
            WorkWithDB = 1,
            LoadFromDb = 2,
            SaveToDB = 3
        };

       static Helper()
       {
           Logger = LogManager.GetCurrentClassLogger();
       }

       public static ILogger Logger { get;}

   
    }
}
