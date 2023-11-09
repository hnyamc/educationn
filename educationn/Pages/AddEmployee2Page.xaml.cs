using educationn.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
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
    /// Логика взаимодействия для AddEmployee2Page.xaml
    /// </summary>
    public partial class AddEmployee2Page : Page
    {
        public static List<Employee> employees { get; set; }
        public static Employee employee = new Employee();
        public static List<Cafedra> cafedras = new List<Cafedra>();
        public static Employee emp { get; set; }
        Employee contextEmployee;
        public AddEmployee2Page(Employee employee)
        {
            InitializeComponent();
            emp = employee;
            contextEmployee = employee;
            var cip = DBConnection.Uchebka1Entities.Cafedra.ToList();
            CipherCB.ItemsSource = cip.ToList();
            CipherCB.DisplayMemberPath = "Cipher";
            InitializeDataInPage();
            
            this.DataContext = this;
        }

        private void InitializeDataInPage()
        {
            employees = new List<Employee>(DBConnection.Uchebka1Entities.Employee.Where(x => x.Tab_number == contextEmployee.Tab_number).ToList());
            TitleCB.ItemsSource = DBConnection.Uchebka1Entities.Employee.Select(x => x.Title).Distinct().ToList();

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextEmployee);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(contextEmployee, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    error += $"{result.ErrorMessage}\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (contextEmployee.Tab_number == 0)
                DBConnection.Uchebka1Entities.Employee.Add(contextEmployee);

            DBConnection.Uchebka1Entities.SaveChanges();
            NavigationService.GoBack();
            
        }

        private void Dobav_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                emp.Photo = File.ReadAllBytes(openFileDialog.FileName);
                Image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
