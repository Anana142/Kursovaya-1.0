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
        public List<Subscription> ListSubscriptions { get => listSubscriptions; set { listSubscriptions = value; Signal(); } }

        public List<string> SortingList { get; set; } = new List<string>() { "Нет сортировки", "Сортировка по фамилии клиента", "Сортировка по активности" };



        public Subscription Selected { get => selected; set { selected = value; Signal(); } }

        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public int SelectedSortNumber { get => selectedSortNumber; set { selectedSortNumber = value; Search(); } }

        public Worker Worker { get; set; }
        public MainPage(Worker worker)
        {
            InitializeComponent();

        


            Worker = worker;
            MyDataGrid.VerticalGridLinesBrush = new SolidColorBrush(Colors.Transparent);
            MyDataGrid.HorizontalGridLinesBrush = new SolidColorBrush(Colors.White);
           
            this.ListSubscriptions = DataBase.GetInstance().Subscriptions.Include(s => s.IdClientNavigation).Include(s => s.IdPeriodNavigation).Include(s => s.Subscriptionservices).Include(s => s.Attendances).ToList();

            foreach (var item in ListSubscriptions)
            {
                DateOnly data = (DateOnly)item.StartDate;

                data = data.AddDays(item.IdPeriodNavigation.Duration);

                string dataTime = DateTime.Now.ToString();

                string[] dataAndTime = dataTime.Split(' ');

                DateOnly datanow = DateOnly.Parse(dataAndTime[0]);

                if (data < datanow || item.UsedVisits <= 0)
                {
                    item.Status = "Неактивен";
                    DataBase.GetInstance().Subscriptions.Update(item);
                    DataBase.GetInstance().SaveChanges();
                }
            }
                DataContext = this;

            
        }

        

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            if ((bool)new YesNoWindow("Вы действительно хотите закрыть приложение?").ShowDialog())
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
        }

        private void Visit(object sender, RoutedEventArgs e)
        {

            if (Selected != null && (bool)new YesNoWindow("Добавить посещение?").ShowDialog())
            {
                
                if (Selected.UsedVisits > 0)
                {
                    Attendance attendance = new Attendance();
                    attendance.IdSubscription = Selected.Id;
                    attendance.Date = DateTime.Now;

                    DataBase.GetInstance().Attendances.Add(attendance);
                    DataBase.GetInstance().SaveChanges();
                    Signal(nameof(Selected));
                    Search();
                }
                if(Selected.UsedVisits <= 0)
                {
                    Selected.Status = "Неактивен";
                    DataBase.GetInstance().Subscriptions.Update(Selected);
                    DataBase.GetInstance().SaveChanges();

                }
                Search();

              }
            



        }
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

        private void TurnOver(object sender, RoutedEventArgs e)
        {
            ListSubscriptions.Reverse();
            Signal(nameof(ListSubscriptions));
            MyDataGrid.Items.Refresh(); 
        }

        private void AddSub(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new NewSubscription(Worker, true);
        }

        
    }
}
