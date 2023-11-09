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
    /// Логика взаимодействия для DisciplinePage.xaml
    /// </summary>
    public partial class DisciplinePage : Page
    {
        public static List<Discipline> disciplines { get; set; }
        public DisciplinePage()
        {
            InitializeComponent();
            disciplines = new List<Discipline>(DBConnection.Uchebka1Entities.Discipline.ToList());
            this.DataContext = this;
            DisciplineLV.ItemsSource = new List<Discipline>(DBConnection.Uchebka1Entities.Discipline.ToList());
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
