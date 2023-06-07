using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWM_WPF
{
    public class GridTwoRow
    {
        public GridTwoRow(string age, string val1, string val2)
        {
            Age = age;
            this.val1 = val1;
            this.val2 = val2;
        }
        public string Age { get; set; }
        public string val1 { get; set; }
        public string val2 { get; set; }
    }
}
