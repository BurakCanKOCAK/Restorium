using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restorium
{
    class LastChoosenTable
    {
        public static string[] Menu = new string[30];
        public static string TableNumber;
        public static string Waiter;
        public static int iskonto;
        public static string musteriAdi;
        public static bool reservation;
        //TableClose Event Variables //////////////////////
        public static decimal cari;
        public static decimal krediKarti;
        public static decimal nakit;
        public static string paraBirimi;
        public static string  lastClosedTableName;
        public static string  lastClosedTableTime;
        public static string  lastClosedTableWaiter;
        public static int     lastClosedTableIskontoOrani;
        public static decimal lastClosedTableTutar;
        public static decimal DefinedEuro;
        public static decimal DefinedDolar;
        public static decimal DefinedGBP;
        public static decimal tip;
        public static decimal tipKredi;
        public static decimal tipNakit;
    }
}
