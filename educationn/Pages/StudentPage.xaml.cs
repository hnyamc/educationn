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
    /// Логика взаимодействия для StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public static List<Student> students { get; set; }
        public static List<Exzamen> exzamens { get; set; }
        Exzamen contextexam;
        Exzamen ex { get; set; }
        public StudentPage(Exzamen exzamen)
        {
            InitializeComponent();
            ex = exzamen;
            contextexam = exzamen;
            InitializeDataInPage();
           
            this.DataContext = this;
            var stud = DBConnection.Uchebka1Entities.Student.ToList();
            StudentCB.ItemsSource = stud.ToList();
            StudentCB.DisplayMemberPath = "FIO";


        }
        private void InitializeDataInPage()
        {
            students = new List<Student>(DBConnection.Uchebka1Entities.Student.ToList());
            exzamens = new List<Exzamen>(DBConnection.Uchebka1Entities.Exzamen.Where(x => x.ID_exz == contextexam.ID_exz).ToList());
            StudentCB.ItemsSource = DBConnection.Uchebka1Entities.Student.ToList();
            ExzamennLV.ItemsSource = new List<Exzamen>(DBConnection.Uchebka1Entities.Exzamen.ToList());
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamPage());
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ExzamennLV.SelectedItem is Exzamen exam)
            {
                DBConnection.Uchebka1Entities.Exzamen.Remove(exam);
                DBConnection.Uchebka1Entities.SaveChanges();
                InitializeDataInPage();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string mark = "2";
            var TBmark = CipCB.SelectedValue as TextBlock;
            if (TBmark != null)
                mark = TBmark.Text;
            if (StudentCB.SelectedItem is Student student)
            {
                var exam = contextexam;
                exam.Student = student;
                exam.Grade = int.Parse(mark);
                var studentInList = exzamens.FirstOrDefault(x => x.Reg_number == student.Reg_number);
                if (studentInList != null)
                {
                    MessageBox.Show("Такой студент уже есть в экзамене");
                    return;
                }
                DBConnection.Uchebka1Entities.Exzamen.Add(exam);
                DBConnection.Uchebka1Entities.SaveChanges();
                Refresh();
                InitializeDataInPage();

            }
            }
        private void Refresh()
        {
            ExzamennLV.ItemsSource = exzamens;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

        }
    }
}
