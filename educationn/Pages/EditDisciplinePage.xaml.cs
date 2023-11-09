using educationn.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace educationn.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditDisciplinePage.xaml
    /// </summary>
    public partial class EditDisciplinePage : Page
    {
        public static Discipline disc {  get; set; }
        public static List<Discipline> disciplines = new List<Discipline>();
        public static List<Cafedra> cafedras = new List<Cafedra>();
        Discipline contextDisciplina;
        private void InitializeDataInPage()
        {
            
            disciplines = new List<Discipline>(DBConnection.Uchebka1Entities.Discipline.Where(x => x.Code_dic == contextDisciplina.Code_dic).ToList());
            cafedras = new List<Cafedra>(DBConnection.Uchebka1Entities.Cafedra.ToList());
        }
    public EditDisciplinePage(Discipline discipline)
        {
            InitializeComponent();
            contextDisciplina = discipline;
            disc = discipline;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextDisciplina);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var disciplina = DBConnection.Uchebka1Entities.Discipline.FirstOrDefault(x => x.Code_dic == contextDisciplina.Code_dic);
            if (disciplina != null && disciplina != contextDisciplina)
                error += "This department shifr already exists";
            if (!Validator.TryValidateObject(contextDisciplina, validationContext, results, true))
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
            if (disciplina == null)
                DBConnection.Uchebka1Entities.Discipline.Add(contextDisciplina);
            DBConnection.Uchebka1Entities.SaveChanges();
        }
    }
}

