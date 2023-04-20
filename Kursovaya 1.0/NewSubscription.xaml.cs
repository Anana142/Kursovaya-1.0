using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для NewSubscription.xaml
    /// </summary>
    public partial class NewSubscription : Page, INotifyPropertyChanged
    {
        private List<Client> clientList;
        private string clientName;
        private string searchText = "";
        private List<Service> serviceList;
        private string servieTitle;
        private List<Worker> workerList;
        private Worker selectedWorker;
        private string workerName;
        private Client newClient = new Client() { Name = "", SurName = "", Patronymic = "" };
        private string birthdayNewClient = "";

        public List<Client> ClientList { get => clientList; set { clientList = value; Signal(); } }
        public List<Service> ServiceList { get => serviceList; set { serviceList = value; Signal(); } }
        public List<Worker> WorkerList { get => workerList; set { workerList = value; Signal(); } }

        public List<Period> PeriodList { get; set; }

        public Client SelectedClient { get; set; }
        public string ClientName { get => clientName; set { clientName = value; Signal(); } }

        public Service SelectedService { get; set; }
        public string ServieTitle { get => servieTitle; set { servieTitle = value; Signal(); } }

        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; Signal(); } }
        public string WorkerName { get => workerName; set { workerName = value; Signal(); } }

        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public Client NewClient { get => newClient; set { newClient = value; Signal(); } }
        public string BirthdayNewClient { get => birthdayNewClient; set { birthdayNewClient = value; SpelledCorrectly(); } }

        public NewSubscription()
        {
            InitializeComponent();
            PeriodList = DataBase.GetInstance().Periods.ToList();
            DataContext = this;
        }

        private void Search()
        {
            if (MyBorder.Visibility == Visibility)
            {
                var result = DataBase.GetInstance().Clients.
                     Where(s => s.SurName.Contains(SearchText) ||
                     s.Name.Contains(SearchText) ||
                     s.Patronymic.Contains(SearchText)
                     );
                ClientList = result.ToList();

                Signal(nameof(ClientList));
            }
            else if (MyBorderService.Visibility == Visibility)
            {
                var result = DataBase.GetInstance().Services.
                    Where(s => s.Title.Contains(SearchText));
                ServiceList = result.ToList();
                Signal(nameof(ServiceList));
            }
            else if (MyBorderWorker.Visibility == Visibility)
            {
                var result = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && (
                s.Name.Contains(SearchText) ||
                s.Surname.Contains(SearchText) ||
                s.Patronymic.Contains(SearchText))
                );
                WorkerList = result.ToList();
                Signal(nameof(WorkerList));
            }
        }

        private void ChooseClient(object sender, RoutedEventArgs e)
        {
            ClientList = DataBase.GetInstance().Clients.ToList();
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Visible;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddClientName(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null)
                ClientName = SelectedClient.SurName.ToString() + " " + SelectedClient.Name.ToString();
        }

        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            MyBorder.Visibility = Visibility.Collapsed;
            NewClientBorder.Visibility = Visibility.Visible;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;
        }

        private void ChooseService(object sender, RoutedEventArgs e)
        {
            ServiceList = DataBase.GetInstance().Services.ToList();
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;
            MyBorderService.Visibility = Visibility.Visible;
        }

        private void AddServiceTitle(object sender, RoutedEventArgs e)
        {
            if (SelectedService != null)
                ServieTitle = SelectedService.Title;
        }

        private void ChooseWorker(object sender, RoutedEventArgs e)
        {
            WorkerList = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2).ToList();
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Collapsed;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Visible;
        }

        private void AddWorkerName(object sender, RoutedEventArgs e)
        {
            if (SelectedWorker != null)
                WorkerName = SelectedWorker.Surname + " " + SelectedWorker.Name;
        }

        private void AddNewClientInClientList(object sender, RoutedEventArgs e)
        {
            if (NewClient != null)
            {
                NewClient.Birthday = DateOnly.Parse(BirthdayNewClient);

                DataBase.GetInstance().Clients.Add(NewClient);
                DataBase.GetInstance().SaveChanges();

                MessageBox.Show("Клиент добавлен");

                NewClient = new Client() { Name = "", SurName = "", Patronymic = "" };
                BirthdayNewClient = "";
            }
            else
                MessageBox.Show("ghkghhjkjkl");


        }

        private void SpelledCorrectly()
        {
            DateOnly data;

            if(DateOnly.TryParse(BirthdayNewClient, out data))
            {
                Birthday.Foreground = new SolidColorBrush(Colors.Black);
                SaveNewClient.IsEnabled = true;
            }
            else
            {
                Birthday.Foreground = new SolidColorBrush(Colors.Red);
                SaveNewClient.IsEnabled = false;
            }
        }
    }
}
