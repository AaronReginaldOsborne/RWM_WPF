using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWM_WPF
{
    public class GridOneRow
    {
        public GridOneRow(string age, string val)
        {
            Age = age;
            this.BenefitToEstate = val;
        }
        public string Age { get; set; }
        public string BenefitToEstate { get; set; }
    }
}
