using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RWM_WPF
{
    /// <summary>
    /// Interaction logic for Insurance_vs_Investment.xaml
    /// </summary>
    public partial class Insurance_vs_Investment : UserControl
    {

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Insurance_vs_Investment()
        {
            InitializeComponent();
            AddList();
            AddGraph();
        }

        private void AddList()
        {
            List<GridTwoRow> GridOne = new List<GridTwoRow>();
            decimal investment = 0.0m;
            int tenYearCounter = 0;

            for (int i = 0; i < (GlobalUtil.AgeIterations * 10); ++i)
            {

                if (i < GlobalUtil.TaxYears)
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * GlobalUtil.WLRate;
                }
                else
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * (1+((GlobalUtil.WLRate-1) * GlobalUtil.TaxBrack));
                }

                //every 10 years
                if ((i+1) % 10 == 0 && i !=0)
                {
                    string AgeTitle = "Age " + (GlobalUtil.Age + ((tenYearCounter+1) * 10));
                    GridOne.Add(new GridTwoRow(AgeTitle, investment.ToString("C"), GlobalUtil.DeathBenif.ElementAt(tenYearCounter).ToString("C")));
                    tenYearCounter++;
                }
            }


            userInfo.ItemsSource = GridOne;
        }


        private void AddGraph()
        {
            decimal investment = 0.0m;
            decimal[] investments = new decimal[GlobalUtil.AgeIterations];
            int tenYearCounter = 0;
            for (int i = 0; i < (investments.Length*10); ++i)
            {
                if (i < GlobalUtil.TaxYears)
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * GlobalUtil.WLRate;
                }
                else
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * (1 + ((GlobalUtil.WLRate - 1) * GlobalUtil.TaxBrack));
                }

                //every 10 years
                if ((i + 1) % 10 == 0 && i != 0)
                {
                    investments[tenYearCounter++] = investment;
                }
            }

            decimal[] deathBenifits = new decimal[GlobalUtil.AgeIterations];
            for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
            {
                deathBenifits[i] = GlobalUtil.DeathBenif.ElementAt(i);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Investment",
                    Values = new ChartValues<decimal> (investments),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Insurance",
                    Values = new ChartValues<decimal>(deathBenifits),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                }
            };

            String[] age = new String[GlobalUtil.AgeIterations];
            for (int i = 0; i < age.Length; ++i)
                age[i] = "Age " + (GlobalUtil.Age + (i * 10));

            Labels = age;
            YFormatter = value => value.ToString("C");

            DataContext = this;
        }
    }
}
