using educationn.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        public static List<Cafedra> cafedras { get; set; }
        public static Employee logdUser;
        public static Cafedra cafedrass = new Cafedra();
        public DepartmentPage()
        {
            InitializeComponent();
            logdUser = DBConnection.loginedUser;
            cafedras = new List<Cafedra>(DB.DBConnection.Uchebka1Entities.Cafedra.Where(x => x.Cipher == DBConnection.loginedUser.Cipher).ToList());
            this.DataContext = this;

            DepartmentLV.ItemsSource = new List<Cafedra>(DB.DBConnection.Uchebka1Entities.Cafedra.ToList());
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var awds = "Наименование: " + DepTB.Text
                + " " + "Код: " + CipTB.Text;


            if (MessageBox.Show(awds, "Проверьте корректность введенных данных", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {

                cafedrass.Name = DepTB.Text.Trim();
                cafedrass.Cipher = CipTB.Text;
                


                DBConnection.Uchebka1Entities.Cafedra.Add(cafedrass);
                DBConnection.Uchebka1Entities.SaveChanges();

            }
        }

        
        

        private void DepartmentLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentLV.SelectedItem is Cafedra cafedra)
            {
                DepartmentLV.SelectedItem = null;
                NavigationService.Navigate(new Discipline2Page(cafedra));
            }
        }

        private void Refresh()
        {
            DepartmentLV.ItemsSource = cafedras;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
