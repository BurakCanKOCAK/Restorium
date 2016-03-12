using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Restorium
{
    class UserLog
    {
        public static string UserName;
       
        public static void WConsole(String logText)
        {
            //Console'a yazdırma fonksiyonu
            //DebugMonitor debug = new DebugMonitor();
            //debug.User_Monitor(":: User Log (" + UserName + ") :: " + logText + " :: ");
            //string textToDebugMonitor = ":: User Log (" + UserName + ") :: " + logText + " :: ";
           // DebugMonitor.text= textToDebugMonitor;
            Console.WriteLine(":: User Log ("+UserName+") :: "+logText+" :: ");
            
        }
       
        public static void WFile(String logText)
        {
            //Todo : Burada dosyaya yaz 
        }
    }
}
