using educationn.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для Discipline2Page.xaml
    /// </summary>
    public partial class Discipline2Page : Page
    {
        public static List<Discipline> disciplines { get; set; }
        public static List<Cafedra> cafedras { get; set;}
        Cafedra contextCafedra;
        public static Cafedra dep { get; set; }
        public static Discipline disciplines1 = new Discipline();

        public Discipline2Page(Cafedra cafedra)
        {
            InitializeComponent();
            contextCafedra = cafedra;
            dep = cafedra;
            InitializeDataInPage();
            
            this.DataContext = this;
            
        }

        private void InitializeDataInPage()
        {
            cafedras = DBConnection.Uchebka1Entities.Cafedra.ToList();        
            disciplines = new List<Discipline>(DBConnection.Uchebka1Entities.Discipline.Where(x => x.Executor == contextCafedra.Cipher).ToList());
            Discipline2LV.ItemsSource = DB.DBConnection.Uchebka1Entities.Discipline.ToList();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartmentPage());
        }

        private void Add2_Click(object sender, RoutedEventArgs e)
        {
            disciplines1.Name = DiscipTB.Text;
            disciplines1.Volume = int.Parse(VolumeTB.Text);
            var t = contextCafedra;
            disciplines1.Executor = t.Cipher;


            DBConnection.Uchebka1Entities.Discipline.Add(disciplines1);
            DBConnection.Uchebka1Entities.SaveChanges();
            InitializeDataInPage();       
        }
        private void Refresh()
        {
            Discipline2LV.ItemsSource = disciplines;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Discipline2LV.SelectedItem is Discipline disciplina)
            {
                DBConnection.Uchebka1Entities.Discipline.Remove(disciplina);
                DBConnection.Uchebka1Entities.SaveChanges();
                InitializeDataInPage();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Discipline2LV.SelectedItem is Discipline disciplina)
            {
                Discipline2LV.SelectedItem = null;
                NavigationService.Navigate(new EditDisciplinePage(disciplina));
            }
        }
    }
}
