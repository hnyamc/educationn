using educationn.DB;
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

namespace educationn.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Employee> employees { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();

        }
        private void LogBTN_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text.Trim();
            string password = PasswordTB.Password.Trim();

            employees = new List<Employee>(DBConnection.Uchebka1Entities.Employee.ToList());
            Employee currentUser = employees.FirstOrDefault(i => i.Login == login && i.Password == password);
            DBConnection.loginedUser = currentUser;
            if (currentUser != null)
            {
                if (currentUser.Title == "преподаватель")
                    NavigationService.Navigate(new ExamPage());
                if (currentUser.Title == "зав. кафедрой")
                    NavigationService.Navigate(new DepartmentPage());
                if (currentUser.Title == "инженер")
                    NavigationService.Navigate(new EmployeePage());
            }
            else
                MessageBox.Show("Неверно:)");
        }

        private void GuestBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinePage());
        }
    }
}
