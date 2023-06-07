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
    /// Interaction logic for BenefitToEstate.xaml
    /// </summary>
    public partial class BenefitToEstate : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public BenefitToEstate()
        {
            InitializeComponent();
            AddGraph();
            AddList();
        }


        private void AddList()
        {
            List<GridThreeRow> GridThree = new List<GridThreeRow>();
            decimal investment = 0.0m;
            decimal investment2 = 0.0m;
            int tenYearCounter = 0;

            for (int i = 0; i < (GlobalUtil.AgeIterations * 10); ++i)
            {

                if (i < GlobalUtil.TaxYears)
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * GlobalUtil.WLRate;

                    investment2 += GlobalUtil.WholeLifePoli;
                    investment2 = investment2 * GlobalUtil.WLRate;
                }
                else
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * (1 + ((GlobalUtil.WLRate - 1) * GlobalUtil.TaxBrack));

                    investment2 += GlobalUtil.WholeLifePoli;
                    investment2 = investment2 * (1 + ((GlobalUtil.WLRate - 1) * GlobalUtil.TaxBrack));
                }

                //every 10 years
                if ((i + 1) % 10 == 0 && i != 0)
                {
                    string AgeTitle = "Age " + (GlobalUtil.Age + ((tenYearCounter + 1) * 10));
                    decimal benefitToEstate = GlobalUtil.DeathBenif.ElementAt(tenYearCounter) - (investment * 0.985m);
                    decimal benefitToEstate2 = GlobalUtil.DeathBenif.ElementAt(tenYearCounter) - (investment2 * 0.985m);
                    decimal ReturnEquivalent = 0.0m;
                    ReturnEquivalent = CalculateReturn(GlobalUtil.DeathBenif.ElementAt(tenYearCounter), i);
                    for (int j = 0; j < 10 * (tenYearCounter + 1); ++j)
                    {
                        benefitToEstate2 *= 0.98m;
                    }
                    GridThree.Add(new GridThreeRow(AgeTitle, benefitToEstate.ToString("C"), benefitToEstate2.ToString("C"), "% " + ReturnEquivalent.ToString("##.#")));
                    tenYearCounter++;
                }
            }


            userInfo.ItemsSource = GridThree;
        }

        private decimal CalculateReturn(decimal futureValue, int iterations)
        {
            iterations += 1;//offset
            futureValue *= 1.015m;
            var presentValue = 0.0m;
            var estimated = 1.01m;

            while (presentValue < futureValue)
            {
                presentValue = 0;
                estimated += 0.01m;

                for (int i = 0; i < iterations; ++i)
                {
                    if (iterations < GlobalUtil.TaxYears)
                    {
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= estimated;
                    }
                    else
                    {
                        var output2 = 1 + (estimated - 1) * GlobalUtil.TaxBrack;
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= output2;
                    }
                }
            }
            estimated -= 0.01m;
            presentValue = 0;
            while (presentValue < futureValue)
            {
                presentValue = 0;
                estimated += 0.001m;

                for (int i = 0; i < iterations; ++i)
                {
                    if (iterations < GlobalUtil.TaxYears)
                    {
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= estimated;
                    }
                    else
                    {
                        var output2 = 1 + (estimated - 1) * GlobalUtil.TaxBrack;
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= output2;
                    }
                }
            }

            estimated -= 0.001m;
            presentValue = 0;
            while (presentValue < futureValue)
            {
                presentValue = 0;
                estimated += 0.0001m;

                for (int i = 0; i < iterations; ++i)
                {
                    if (iterations < GlobalUtil.TaxYears)
                    {
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= estimated;
                    }
                    else
                    {
                        var output2 = 1 + (estimated - 1) * GlobalUtil.TaxBrack;
                        presentValue += GlobalUtil.WholeLifePoli;
                        presentValue *= output2;
                    }
                }
            }

            estimated -= 1;
            estimated *= 100m;
            return estimated;
        }
        private void AddGraph()
        {

            decimal investment = 0.0m;
            decimal investment2 = 0.0m;
            int tenYearCounter = 0;
            decimal[] investments = new decimal[GlobalUtil.AgeIterations];
            decimal[] investments2 = new decimal[GlobalUtil.AgeIterations];

            for (int i = 0; i < (GlobalUtil.AgeIterations * 10); ++i)
            {
                if (i < GlobalUtil.TaxYears)
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * GlobalUtil.WLRate;

                    investment2 += GlobalUtil.WholeLifePoli;
                    investment2 = investment2 * GlobalUtil.WLRate;
                }
                else
                {
                    investment += GlobalUtil.WholeLifePoli;
                    investment = investment * (1 + ((GlobalUtil.WLRate - 1) * GlobalUtil.TaxBrack));

                    investment2 += GlobalUtil.WholeLifePoli;
                    investment2 = investment2 * (1 + ((GlobalUtil.WLRate - 1) * GlobalUtil.TaxBrack));
                }
                //every 10 years
                if ((i + 1) % 10 == 0 && i != 0)
                {
                    string AgeTitle = "Age " + (GlobalUtil.Age + ((tenYearCounter + 1) * 10));
                    decimal benefitToEstate = GlobalUtil.DeathBenif.ElementAt(tenYearCounter) - (investment * 0.985m);
                    decimal benefitToEstate2 = GlobalUtil.DeathBenif.ElementAt(tenYearCounter) - (investment2 * 0.985m);
                    decimal ReturnEquivalent = 0.0m;
                    ReturnEquivalent = CalculateReturn(GlobalUtil.DeathBenif.ElementAt(tenYearCounter), i);
                    for (int j = 0; j < 10 * (tenYearCounter + 1); ++j)
                    {
                        benefitToEstate2 *= 0.98m;
                    }
                    investments[tenYearCounter] = benefitToEstate;
                    investments2[tenYearCounter] = benefitToEstate2;
                    tenYearCounter++;
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Benefit To Estate",
                    Values = new ChartValues<decimal>(investments),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "After 2% inflation",
                    Values = new ChartValues<decimal>(investments2),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
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
