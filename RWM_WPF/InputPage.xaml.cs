using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for InputPage.xaml
    /// </summary>
    public partial class InputPage : UserControl
    {
        public InputPage()
        {
            InitializeComponent();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalUtil.WholeLifePoli = decimal.Parse(tbWholeLifePolicy.Text);
                GlobalUtil.CheapTerm100Poli = decimal.Parse(tbCheapestPolicy.Text);
                GlobalUtil.WLRate = decimal.Parse(tbRateOfReturn.Text)/100 + 1;
                GlobalUtil.IsTaxable = cbTaxAble.IsChecked;
                GlobalUtil.TaxYears = Int32.Parse(tbYears.Text);
                GlobalUtil.TaxBrack = decimal.Parse(tbBracket.Text);
                GlobalUtil.ExpectAnnual = decimal.Parse(tbAnnualInflation.Text);

                GlobalUtil.DeathBenif.Clear();
                GlobalUtil.CashSurrend.Clear();
                for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
                {
                    TextBox tbDeath = (TextBox)this.deathWrap.FindName("tbDeath"+i);
                    GlobalUtil.DeathBenif.Add(decimal.Parse(tbDeath.Text));
                    TextBox tbCash = (TextBox)this.cashWrap.FindName("tbCash" + i);
                    GlobalUtil.CashSurrend.Add(decimal.Parse(tbCash.Text));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error missing fields");
            }
        }

        private void TbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbAge_LostFocus(object sender, RoutedEventArgs e)
        {
            //NULL CHECK
            if (tbAge.Text != "")
            {
                GlobalUtil.Age = Int32.Parse(tbAge.Text);
                if (GlobalUtil.Age > 0)
                {
                    GlobalUtil.Age += 10;
                    deathWrap.Children.Clear();
                    cashWrap.Children.Clear();
                    GlobalUtil.DeathBenif.Clear();
                    GlobalUtil.CashSurrend.Clear();
                    GlobalUtil.AgeIterations = (int)Math.Ceiling(((99 - GlobalUtil.Age) / 10.0));
                    for (int i = 0; i < GlobalUtil.AgeIterations; ++i)
                    {
                        AddDynamicTextBlock(i, "tbDeath" + i, deathWrap);
                        GlobalUtil.DeathBenif.Add(0);

                        AddDynamicTextBlock(i, "tbCash" + i, cashWrap);
                        GlobalUtil.CashSurrend.Add(0);

                    }

                    //set default values
                }
            }
        }

        private void AddDynamicTextBlock(int index, string name, WrapPanel wrapPanel)
        {
            StackPanel stack = new StackPanel();
            TextBox txtDeath = new TextBox();
            HintAssist.SetHint(txtDeath, "Year " + ((index * 10) + GlobalUtil.Age));
            txtDeath.Style = (Style)FindResource("MaterialDesignFloatingHintTextBox");
            txtDeath.Name = name;
            wrapPanel.RegisterName(txtDeath.Name, txtDeath);
            txtDeath.MaxLength = 50;
            txtDeath.MinWidth = 300;
            txtDeath.Margin = new Thickness(20, 20, 20, 20);
            stack.Children.Add(txtDeath);
            wrapPanel.Children.Add(stack);
        }
    }
}
