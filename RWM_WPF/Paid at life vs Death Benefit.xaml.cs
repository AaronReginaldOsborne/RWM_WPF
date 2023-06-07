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
    /// Interaction logic for Paid_at_life_vs_Death_Benefit.xaml
    /// </summary>
    public partial class Paid_at_life_vs_Death_Benefit : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Paid_at_life_vs_Death_Benefit()
        {
            InitializeComponent();
            AddGraph();
            AddList();
        }


        private void AddList()
        {
            List<GridThreeRow> GridOne = new List<GridThreeRow>();
            decimal totalPremium = 0.0m;
            for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
            {
                string AgeTitle = "Age " + (GlobalUtil.Age + (i * 10));
                totalPremium += GlobalUtil.WholeLifePoli * ((i + 1) * 10);

                GridOne.Add(new GridThreeRow(AgeTitle, totalPremium.ToString("C"), GlobalUtil.DeathBenif.ElementAt(i).ToString("C"), GlobalUtil.CashSurrend.ElementAt(i).ToString("C")));
            }


            userInfo.ItemsSource = GridOne;
        }

        private void AddGraph()
        {
            decimal totalPremium = 0.0m;
            decimal[] totalPremiums = new decimal[GlobalUtil.AgeIterations];
            for (int i = 0; i < totalPremiums.Length; ++i)
            {
                totalPremium += GlobalUtil.WholeLifePoli * ((i+1) * 10);

                totalPremiums[i] = totalPremium;
            }

            decimal[] deathBenifits = new decimal[GlobalUtil.AgeIterations];
            for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
            {
                deathBenifits[i] = GlobalUtil.DeathBenif.ElementAt(i);
            }

            decimal[] cashSurrender = new decimal[GlobalUtil.AgeIterations];
            for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
            {
                cashSurrender[i] = GlobalUtil.CashSurrend.ElementAt(i);
            }


            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Investment",
                    Values = new ChartValues<decimal> (totalPremiums),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Death Benifits",
                    Values = new ChartValues<decimal>(deathBenifits),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Cash Value",
                    Values = new ChartValues<decimal>(cashSurrender),
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
