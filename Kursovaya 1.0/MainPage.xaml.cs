using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private Subscription selected;
        private string searchText = "";
        private int selectedSortNumber = 0;
        private List<Subscription> listSubscriptions;

        public DataBase DataBase { get; set; } = new DataBase();
        public List<Subscription> ListSubscriptions { get => listSubscriptions; set => listSubscriptions = value; }

        public List<string> SortingList { get; set; } = new List<string>() { "Нет сортировки", "Сортировка по Фамилии клиента", "Сортировка по активности", "Сортировка по Услуге" };



        public Subscription Selected { get => selected; set { selected = value; Signal(); } }

        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public int SelectedSortNumber { get => selectedSortNumber; set { selectedSortNumber = value; Search(); } }
        public MainPage()
        {
            InitializeComponent();

            this.ListSubscriptions = DataBase.GetInstance().Subscriptions.Include(s => s.IdClientNavigation).Include(s => s.IdPeriodNavigation).Include(s => s.IdServices).Include(s => s.Attendances).Include(s => s.IdDiscountNavigation).ToList();


            DataContext = this;

            

        }

        

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            var windows = Application.Current.Windows.Cast<Window>().ToList();



            foreach (var window in windows)
            {
                if (window == windows.Last())
                {
                    new MainWindow().Show();
                    window.Close();
                    break;
                }


                else
                    window.Close();
            }
        }

        private void Visit(object sender, RoutedEventArgs e)
        {
            Attendance attendance = new Attendance();
            attendance.IdSubscription = Selected.Id;
            attendance.Date = DateTime.Now;

            DataBase.GetInstance().Attendances.Add(attendance);
            DataBase.GetInstance().SaveChanges();
            Signal(nameof(Selected));
        }


        /*private void Search()
        {
            var result = DB.GetInstance().Products.
                Include(s => s.IdProviderNavigation).
                Include(s => s.IdManufacturerNavigation).
                Include(s => s.IdCategoryNavigation).Where(s =>
                    s.IdProviderNavigation.Title.Contains(searchText) ||
                    s.IdManufacturerNavigation.Title.Contains(searchText) ||
                    s.Article.Contains(searchText) ||
                    s.IdCategoryNavigation.Title.Contains(searchText) ||
                    s.ProductDiscription.Contains(searchText) ||
                    s.Title.Contains(searchText)
                );
            if (SelectedManufacturer.Id != 0)
                result = result.Where(s => s.IdManufacturer == SelectedManufacturer.Id);
            if (SelectedSort == 2)
                result = result.OrderByDescending(s => s.Cost);
            else if (SelectedSort == 1)
                result = result.OrderBy(s => s.Cost);
            DataProduct = result.ToList();

        
            Signal(nameof(CountProducts));
            Signal(nameof(DataProduct));
        }*/

        private void Search()
        {
            List<Subscription> list = new List<Subscription>();

           var result = DataBase.GetInstance().Subscriptions.
                Include(s => s.IdClientNavigation).Where(s =>
                    s.IdClientNavigation.SurName.Contains(SearchText));
            if (SelectedSortNumber == 0)
                list = result.ToList();
            else if (SelectedSortNumber == 1)
                list = result.OrderBy(s => s.IdClientNavigation.SurName).ToList();
            else if (SelectedSortNumber == 2)
                list = result.OrderBy(s => s.Status).ToList();
            else if (SelectedSortNumber == 3) {
                list = result.ToList();
                list.OrderBy(s => s.ServiceTitle).ToList();
            }

            ListSubscriptions = list;

            Signal(nameof(ListSubscriptions));
        }


    }
}
