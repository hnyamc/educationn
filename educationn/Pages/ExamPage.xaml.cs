using educationn.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для ExamPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        public static List<Exzamen> exzamens { get; set; }
        public static List<Discipline> disciplines { get; set; }
        public static List<Employee> employees { get; set; }
        public static Employee logdUser;
        public ExamPage()
        {
            InitializeComponent();
            EmpNameTB.Text = DBConnection.loginedUser.FIO;
            exzamens = new List<Exzamen>(DBConnection.Uchebka1Entities.Exzamen.Where(x => x.Tab_number == DBConnection.loginedUser.Tab_number).ToList());
            disciplines = new List<Discipline>(DBConnection.Uchebka1Entities.Discipline.ToList());
            employees = new List<Employee>(DBConnection.Uchebka1Entities.Employee.ToList());
            logdUser = DBConnection.loginedUser;
            this.DataContext = this;
            ExamLV.ItemsSource = new List<Exzamen>(DBConnection.Uchebka1Entities.Exzamen.ToList());
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void ExamLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExamLV.SelectedItem is Exzamen exzamen)
            {
                ExamLV.SelectedItem = null;
                NavigationService.Navigate(new StudentPage(exzamen));
            }
        }
            private void Refresh()
            {
                ExamLV.ItemsSource = exzamens;
            }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Ссылка на XL таблицу
            string soucer_xl = "https://docs.google.com/spreadsheets/d/1UI5mZag-J64ANPp5hpSNzqaAVHBIFrypj2X5hJLQ6j0/edit#gid=0";
            // Создание переменной библиотеки QRCoder
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            // Присваеваем значиения
            QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            // переводим в Qr
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);
            /// Создание картинки
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                imageQr.Source = bitmapimage;
            }
        }
    }
}
