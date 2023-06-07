using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWM_WPF
{
    public class GridThreeRow
    {
        public GridThreeRow(string age, string val1, string val2, string val3)
        {
            Age = age;
            this.val1 = val1;
            this.val2 = val2;
            this.val3 = val3;
        }
        public string Age { get; set; }
        public string val1 { get; set; }
        public string val2 { get; set; }
        public string val3 { get; set; }
    }
}
