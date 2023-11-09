using educationn.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {

        public static List<Employee> employees { get; set; }
        public static List<Cafedra> cafedras { get; set; }
        public static Employee emp = new Employee();

        public AddEmployeePage()
        {
            InitializeComponent();
            employees = new List<Employee>(DBConnection.Uchebka1Entities.Employee.ToList());
            cafedras = new List<Cafedra>(DBConnection.Uchebka1Entities.Cafedra.ToList());
            this.DataContext = this;
            var cip = DBConnection.Uchebka1Entities.Cafedra.ToList();
            CipherCB.ItemsSource = cip.ToList();
            CipherCB.DisplayMemberPath = "Cipher";
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            var awds = "ФИО: " + FIOTB.Text;


            if (MessageBox.Show(awds, "Проверьте корректность введенных данных", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {

                emp.FIO = FIOTB.Text;

                var t = TitleCB.SelectedItem as TextBlock;
                emp.Title = TitleCB.Text;

                var a = CipherCB.SelectedItem as Cafedra;
                emp.Cipher = a.Cipher;
                DBConnection.Uchebka1Entities.Employee.Add(emp);
                DBConnection.Uchebka1Entities.SaveChanges();
            }
        }
    }
}
        


            
                

            


