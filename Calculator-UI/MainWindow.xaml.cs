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

namespace Calculator_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach(dateChangeOptions timePeriod in Enum.GetValues(typeof(dateChangeOptions)))
            {
                timePeriodDD.Items.Add(timePeriod);
            }
            timePeriodDD.Text = "Day";
            curDate.Content = DateTime.Today.ToString("dd/MM/yyyy");
        }
        DateTime lastStateCalander = DateTime.Today;
        private short dateChangeOption = 0;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calander.DisplayDate = lastStateCalander;
            tryChangeDate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tryChangeDate();
            lastStateCalander = Calander.DisplayDate;
            curDate.Content = lastStateCalander.ToString("dd/MM/yyyy");
            tempcurDate.Content = "";
        }

        private void tryChangeDate()
        {
            Calander.DisplayDate = lastStateCalander;
            error.Content = "";
            if (int.TryParse(toAddBox.Text, out int toAdd))
            {
                switch (timePeriodDD.Text)
                {
                    case "Day":
                        Calander.DisplayDate = Calander.DisplayDate.AddDays(toAdd);
                        break;
                    case "Month":
                        Calander.DisplayDate = Calander.DisplayDate.AddMonths(toAdd);
                        break;
                    case "Year":
                        Calander.DisplayDate = Calander.DisplayDate.AddYears(toAdd);
                        break;
                }
                tempcurDate.Content = Calander.DisplayDate.ToString("dd/MM/yyyy");
            }
            else if(toAddBox.Text != "")
            {
                error.Content = "Error: please input an integer";
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            error.Content = "";
            try {
                NumericalCalc calc = new NumericalCalc();
                ans.Content = calc.StartNumCalc(usrInputBox.Text).ToString();
            }
            catch(UnsupportedOperatorException)
            {
                error.Content = "Unsupported operator used.";
                ans.Content = "";
            }
            catch (DivideByZeroException)
            {
                error.Content = "Cannot divide by zero.";
                ans.Content = "undefined";
            }
            catch
            {
                ans.Content = "..";
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(ans.Content.ToString(), out double result))
            {
                usrInputBox.Text = ans.Content.ToString();
            }
            else
            {
                error.Content = "Please enter a valid calculation.";
            }
        }
        public static Boolean IsValid(string input, int type)
        {
            //type 0 to check operator, 1 for int, 2 for DateTime.           
            if (input.Length == 0)
            {
                Console.WriteLine("Invalid input! Please try again.");
                Console.WriteLine();
                return false;
            }

            switch (type)
            {
                case 0:
                    if (input.ToCharArray()[0] == '+' | input.ToCharArray()[0] == '-' | input.ToCharArray()[0] == '*' | input.ToCharArray()[0] == '/')
                    {
                        return true;
                    }
                    break;
                case 1:
                    double tmp;
                    if (double.TryParse(input, out tmp))
                    {
                        return true;
                    }
                    break;
                case 2:
                    DateTime tmpDate = new DateTime();
                    if (DateTime.TryParse(input, out tmpDate))
                    {
                        return true;
                    }
                    break;
            }
            Console.WriteLine("Invalid input! Please try again.");
            Console.WriteLine();
            return false;
        }

        private enum dateChangeOptions
        {
            Day = 0,
            Month = 1,
            Year = 2
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toAddTxt.Content = timePeriodDD.SelectedValue + "s to add:";
            Calander.DisplayDate = lastStateCalander;
            System.Threading.Thread.Sleep(50);
            tryChangeDate();
        }
    }
}
