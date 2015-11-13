using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restorium
{
    class UserLog
    {
        public static string UserName;

        public static void WConsole(String logText)
        {
            //Console'a yazdırma fonksiyonu
            Console.WriteLine("::"+logText+" Authantication Succesfull::");
            Console.WriteLine(":::::" + UserName + "::::::::");
        }
        public static void WFile(String logText)
        {
            //Todo : Burada dosyaya yaz 
        }
    }
}
