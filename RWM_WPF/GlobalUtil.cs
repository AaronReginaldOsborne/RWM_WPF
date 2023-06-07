using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWM_WPF
{
    public static class GlobalUtil
    {
        public static int Age { get; set; }
        public static int AgeIterations { get; set; }
        public static decimal WholeLifePoli { get; set; }
        public static decimal CheapTerm100Poli { get; set; }
        public static decimal WLRate { get; set; }
        public static bool? IsTaxable { get; set; }
        public static int TaxYears { get; set; }
        public static decimal TaxBrack { get; set; }
        public static decimal ExpectAnnual { get; set; }
        public static List<decimal> DeathBenif { get; set; }
        public static List<decimal> CashSurrend { get; set; }
    }
}
