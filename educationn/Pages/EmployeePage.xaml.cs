using educationn.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace educationn.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public static List<Employee> employees { get; set; }
        public static Employee logdUser;

        public EmployeePage()
        {
            InitializeComponent();
            EmpNameTB.Text = DBConnection.loginedUser.FIO;             
            InitializeDataInPage();
            logdUser = DBConnection.loginedUser;
            this.DataContext = this;
        }
        private void InitializeDataInPage()
        {
            employees = new List<Employee>(DBConnection.Uchebka1Entities.Employee.ToList());
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }


        private void Refresh()
        {
           EmployeeLV.ItemsSource = DBConnection.Uchebka1Entities.Employee.ToList();
        }


        private void Addd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeLV.SelectedItem is Employee employee)
            {
                EmployeeLV.SelectedItem = null;
                NavigationService.Navigate(new AddEmployee2Page(employee));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeLV.SelectedItem is Employee employee)
            {
                DBConnection.Uchebka1Entities.Employee.Remove(employee);
                DBConnection.Uchebka1Entities.SaveChanges();
                Refresh();
            }
        }
    }
}
