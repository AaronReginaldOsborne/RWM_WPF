using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RWM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool StateClosed = true;

        public MainWindow()
        {
            InitializeComponent();
            AddMenuItems();
            GlobalUtil.DeathBenif = new List<decimal>();
            GlobalUtil.CashSurrend = new List<decimal>();
#if DEBUG
            //set test data here
            TestData();
#endif

            Main.Content = new InputPage();
        }

        private void TestData()
        {
            GlobalUtil.Age = 31;
            GlobalUtil.AgeIterations = (int)Math.Ceiling(((99 - GlobalUtil.Age) / 10.0)) -1;
            GlobalUtil.WholeLifePoli = 1181;
            GlobalUtil.CheapTerm100Poli = 1000;
            GlobalUtil.WLRate = decimal.Parse("6") / 100 + 1;
            GlobalUtil.IsTaxable = false;
            GlobalUtil.TaxYears = 5;
            GlobalUtil.TaxBrack = 1 - ((decimal.Parse("31.5") / 2.0m) / 100.00m);
            GlobalUtil.ExpectAnnual = 1 - (decimal.Parse("2") / 100);

            GlobalUtil.DeathBenif.Add(150000);
            GlobalUtil.DeathBenif.Add(150000);
            GlobalUtil.DeathBenif.Add(151100);
            GlobalUtil.DeathBenif.Add(200600);
            GlobalUtil.DeathBenif.Add(275100);
            GlobalUtil.DeathBenif.Add(386300);

            GlobalUtil.CashSurrend.Add(3100);
            GlobalUtil.CashSurrend.Add(26700);
            GlobalUtil.CashSurrend.Add(62600);
            GlobalUtil.CashSurrend.Add(121200);
            GlobalUtil.CashSurrend.Add(212900);
            GlobalUtil.CashSurrend.Add(346500);


        }


        private void AddMenuItems()
        {
            List<MenuItem> menu = new List<MenuItem>();

            menu.Add(new MenuItem("Input Fields", PackIconKind.Import));
            menu.Add(new MenuItem("Grid one", PackIconKind.ChartMultiline));
            menu.Add(new MenuItem("Grid two", PackIconKind.ChartFinance));
            menu.Add(new MenuItem("Grid three", PackIconKind.ChartLineVariant));
            menu.Add(new MenuItem("Savings Page", PackIconKind.ChartLineStacked));

            ListViewMenu.ItemsSource = menu;

        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            ListViewItem lbi = sender as ListViewItem;
            if (lbi != null)
            {
                MenuItem mi = lbi.Content as MenuItem;
                switch (mi.Name)
                {
                    case "Input Fields":
                        Main.Content = new InputPage();
                        break;
                    case "Grid one":
                        Main.Content = new Insurance_vs_Investment();
                        break;
                    case "Grid two":
                        Main.Content = new Investment_vs_annual_premium();
                        break;
                    case "Grid three":
                        Main.Content = new BenefitToEstate();
                        break;
                    case "Savings Page":
                        Main.Content = new Paid_at_life_vs_Death_Benefit();
                        break;
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        private void ButtonMinScreen_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void ButtonFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Maximized)
                WindowState = System.Windows.WindowState.Normal;
            else
                WindowState = System.Windows.WindowState.Maximized;
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
