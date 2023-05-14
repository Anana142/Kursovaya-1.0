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
        private string searchText = "";
        private List<Service> serviceList = new List<Service>();
        private List<Worker> workerList = new List<Worker>();
        private Worker selectedWorker;
        private Client newClient = new Client() { Name = "", SurName = "", Patronymic = "" };
        private string birthdayNewClient = "";
        private List<Serviceworkersgraph> graphList;
        private Service selectedService;
        private Service addSelectedService;
        private Worker addSelectedWorker;
        private List<Service> addServiceList;
        private List<Worker> addWorkerList;
        private Client addClientName;
        private Serviceworkersgraph selectedGraph;
        private List<Serviceworkersgraph> addGraphList = new List<Serviceworkersgraph>();
        private decimal price;
        private int selectedIndexCombobox;

        public List<Client> ClientList { get => clientList; set { clientList = value; Signal(); } }
        public List<Service> ServiceList { get => serviceList; set { serviceList = value; Signal(); } }
        public List<Service> AddServiceList { get => addServiceList; set { addServiceList = value; Signal(); } }
        public List<Worker> WorkerList { get => workerList; set { workerList = value; Signal(); } }
        public List<Worker> AddWorkerList { get => addWorkerList; set { addWorkerList = value; Signal(); } }
        public List<Serviceworkersgraph> GraphList { get => graphList; set { graphList = value; Signal(); } }

        public List<Period> PeriodList { get; set; }

        public Client SelectedClient { get; set; }
        public Client AddClient { get => addClientName; set { addClientName = value; Signal(); } }
        public Service SelectedService { get => selectedService; set { selectedService = value; Signal(); } }

        public Service AddSelectedService { get => addSelectedService; set { addSelectedService = value; Signal(); DeleteGraph(); } }
        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; Signal(); } }
        public Worker AddSelectedWorker { get => addSelectedWorker; set { addSelectedWorker = value; Signal(); DeleteGraph(); } }

        public Serviceworkersgraph SelectedGraph { get => selectedGraph; set { selectedGraph = value; Signal(); } }
        public List<Serviceworkersgraph> AddGraphList { get => addGraphList; set { addGraphList = value; Signal(); } }

        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public Client NewClient { get => newClient; set { newClient = value; Signal(); } }
        public string BirthdayNewClient { get => birthdayNewClient; set { birthdayNewClient = value; SpelledCorrectly(); Signal(); } }
        public int SelectedIndexCombobox { get => selectedIndexCombobox; set { selectedIndexCombobox = value; DeleteGraph(); } }
        public Serviceworkersgraph RemoveFromListBox { get; set; }
        public decimal Price { get => price; set { price = value; Signal(); } }
        public Worker Worker { get; set; }
        public bool IsTrue { get; set; }

        public NewSubscription(Worker worker, bool istrue)
        {
            InitializeComponent();
            Worker = worker;
            PeriodList = DataBase.GetInstance().Periods.ToList();
            IsTrue = istrue;
            ShowButton(IsTrue);
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
                if (AddSelectedWorker != null)
                {
                    AddServiceList = ServiceList;
                    var res = AddServiceList.Where(s => s.Title.Contains(SearchText));
                    AddServiceList = res.ToList();
                    Signal(nameof(AddServiceList));
                }
                else if (AddSelectedWorker == null)
                {
                    var result = DataBase.GetInstance().Services.
                        Where(s => s.Title.Contains(SearchText));
                    ServiceList = result.ToList();
                    AddServiceList = ServiceList;
                    Signal(nameof(AddServiceList));
                }
            }
            else if (MyBorderWorker.Visibility == Visibility)
            {
                if (AddSelectedService == null)
                {
                    var result = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && (
                    s.Name.Contains(SearchText) ||
                    s.Surname.Contains(SearchText) ||
                    s.Patronymic.Contains(SearchText))
                    );
                    WorkerList = result.ToList();
                    Signal(nameof(WorkerList));
                }
                else if (AddSelectedService != null)
                {
                    AddWorkerList = WorkerList;
                    var res = AddWorkerList.Where(s => s.IdPost == 2 && (
                   s.Name.Contains(SearchText) ||
                   s.Surname.Contains(SearchText) ||
                   s.Patronymic.Contains(SearchText))
                   );
                    AddWorkerList = res.ToList();
                    Signal(nameof(AddWorkerList));
                }
            }
        }

        private void ChooseClient(object sender, RoutedEventArgs e)
        {
            ClientList = DataBase.GetInstance().Clients.ToList();
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Visible;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;
            MyBorderGraph.Visibility = Visibility.Collapsed;

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddClientName(object sender, RoutedEventArgs e)
        {
            AddClient = SelectedClient;

        }

        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            MyBorder.Visibility = Visibility.Collapsed;
            NewClientBorder.Visibility = Visibility.Visible;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;
            MyBorderGraph.Visibility = Visibility.Collapsed;
        }

        private void ChooseService(object sender, RoutedEventArgs e)
        {
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Collapsed;
            MyBorderService.Visibility = Visibility.Visible;
            MyBorderGraph.Visibility = Visibility.Collapsed;

            ServiceList = new List<Service>();

            List<Serviceworkersgraph> swg = new List<Serviceworkersgraph>();

            if (AddSelectedWorker == null)
            {
                swg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdServiceNavigation).Where(s => s.IsDeleted != true).ToList();


                foreach (var serv in swg)
                {
                    if (ServiceList.Count == 0 || ServiceList.FirstOrDefault(s => s.Id == serv.IdService) == null)
                    {
                        ServiceList.Add(serv.IdServiceNavigation);
                        AddServiceList = ServiceList;
                    }
                }

                SelectedService = AddSelectedService;
            }
            else if (AddSelectedWorker != null)
            {

                swg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdServiceNavigation).Where(s => s.IdWorker == AddSelectedWorker.Id && s.IsDeleted != true).ToList();

                foreach (var serv in swg)
                {
                    if (ServiceList.Count == 0 || ServiceList.FirstOrDefault(s => s.Id == serv.IdService) == null)
                    {
                        ServiceList.Add(serv.IdServiceNavigation);
                        AddServiceList = ServiceList;
                    }
                }
            }
            MyDataGridService.Items.Refresh();
        }

        private void AddServiceTitle(object sender, RoutedEventArgs e)
        {
            AddSelectedService = SelectedService;
        }

        private void ChooseWorker(object sender, RoutedEventArgs e)
        {

            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Collapsed;
            MyBorderService.Visibility = Visibility.Collapsed;
            MyBorderWorker.Visibility = Visibility.Visible;
            MyBorderGraph.Visibility = Visibility.Collapsed;

            WorkerList = new List<Worker>();

            List<Serviceworkersgraph> swg = new List<Serviceworkersgraph>();

            if (AddSelectedService == null)
            {
                swg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation).Where(s => s.IsDeleted != true).ToList();

                foreach (var serv in swg)
                {
                    if (WorkerList.Count == 0 || WorkerList.FirstOrDefault(s => s.Id == serv.IdWorker) == null)
                    {
                        WorkerList.Add(serv.IdWorkerNavigation);
                        AddWorkerList = WorkerList;
                    }
                }
            }
            else if (AddSelectedService != null)
            {
                swg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdServiceNavigation).Where(s => s.IdService == AddSelectedService.Id && s.IsDeleted != true).ToList();

                foreach (var serv in swg)
                {
                    if (WorkerList.Count == 0 || WorkerList.FirstOrDefault(s => s.Id == serv.IdWorker) == null)
                    {
                        WorkerList.Add(serv.IdWorkerNavigation);
                        AddWorkerList = WorkerList;
                    }
                }
            }

            MyDataGridWorker.Items.Refresh();
        }

        private void AddWorkerName(object sender, RoutedEventArgs e)
        {
            AddSelectedWorker = SelectedWorker;
        }

        private void AddNewClientInClientList(object sender, RoutedEventArgs e)
        {
            if (NewClient != null && BirthdayNewClient != "")
            {
                NewClient.Birthday = DateOnly.Parse(BirthdayNewClient);

                DataBase.GetInstance().Clients.Add(NewClient);
                DataBase.GetInstance().SaveChanges();

                MessageBox.Show("Клиент добавлен");

                NewClient = new Client() { Name = "", SurName = "", Patronymic = "", Birthday = null };
                BirthdayNewClient = "";
            }
            else
                MessageBox.Show("Ошибка :(");


        }

        private void SpelledCorrectly()
        {
            DateOnly data;

            if (DateOnly.TryParse(BirthdayNewClient, out data))
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

        private void ChooseGraph(object sender, RoutedEventArgs e)
        {


            if (AddSelectedService == null)
                MessageBox.Show("Необходимо выбрать услугу");
            else
            {
                MyBorderGraph.Visibility = Visibility.Visible;
                NewClientBorder.Visibility = Visibility.Collapsed;
                MyBorder.Visibility = Visibility.Collapsed;
                MyBorderService.Visibility = Visibility.Collapsed;
                MyBorderWorker.Visibility = Visibility.Collapsed;

                List<Serviceworkersgraph> list = new List<Serviceworkersgraph>();
                if (AddSelectedWorker != null && AddSelectedService != null)
                {

                    GraphList = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdGraphNavigation).Where(s => s.IdService == AddSelectedService.Id && s.IdWorker == AddSelectedWorker.Id && s.IsDeleted != true).ToList();
                    foreach (var graph in GraphList)
                    {
                        if (graph.IdServiceNavigation.NumberOfPersons > graph.Busy)
                        {
                            list.Add(graph);
                        }
                    }

                }
                else if (AddSelectedService != null)
                {
                    GraphList = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdGraphNavigation).Where(s => s.IdService == AddSelectedService.Id && s.IsDeleted != true).ToList();
                    foreach (var graph in GraphList)
                    {
                        if (graph.IdServiceNavigation.NumberOfPersons > graph.Busy)
                        {
                            list.Add(graph);
                        }
                    }
                }
                GraphList = list;

            }
        }

        private void AddGraph(object sender, RoutedEventArgs e)
        {
            if (SelectedIndexCombobox != 0)
            {
                if (SelectedGraph != null)
                {
                    if (AddGraphList.Count() == 0 || AddGraphList.FirstOrDefault(s => s.Id == SelectedGraph.Id) == null)
                    {
                        AddGraphList.Add(SelectedGraph);
                        Signal(nameof(AddGraphList));
                        ListBoxGraph.Items.Refresh();
                        RemoveGraphButton.Visibility = Visibility.Visible;

                    }
                }
            }
            else
            {
                if (AddGraphList.Count < 1)
                {
                    AddGraphList.Add(SelectedGraph);
                    Signal(nameof(AddGraphList));
                    ListBoxGraph.Items.Refresh();
                    RemoveGraphButton.Visibility = Visibility.Visible;
                }
            }
            PriceMaker();
        }

        private void RemoveGraph(object sender, RoutedEventArgs e)
        {
            if (RemoveFromListBox != null)
            {
                AddGraphList.Remove(RemoveFromListBox);
                ListBoxGraph.Items.Refresh();
            }
            if (AddGraphList.Count == 0)
                RemoveGraphButton.Visibility = Visibility.Collapsed;
            PriceMaker();
        }

        public void PriceMaker()
        {
            if (SelectedWorker != null && SelectedService != null)
            {

                int CountWeek = 0;

                if (SelectedIndexCombobox == 0)
                    CountWeek = 1;
                else
                {
                    CountWeek = PeriodList[SelectedIndexCombobox].Duration / 7;
                }

                Price = (decimal)(SelectedService.PricePerHour) * AddGraphList.Count() * CountWeek;
                Signal(nameof(Price));

            }
        }
        public DateOnly DateOnlyNow()
        {
            string dataTime = DateTime.Now.ToString();

            string[] dataAndTime = dataTime.Split(' ');

            DateOnly data = DateOnly.Parse(dataAndTime[0]);

            return data;
        }

        private void AddSubscription(object sender, RoutedEventArgs e)
        {
            if (AddSelectedService != null && AddSelectedWorker != null && AddClient != null && AddGraphList.Count() != 0 && Worker != null)
            {
                int CountWeek = 0;

                if (SelectedIndexCombobox == 0)
                    CountWeek = 1;
                else
                {
                    CountWeek = PeriodList[SelectedIndexCombobox].Duration / 7;
                }

                Subscription sub = new Subscription()
                {
                    IdClient = AddClient.Id,
                    IdPeriod = PeriodList[SelectedIndexCombobox].Id,
                    TotalVisits = AddGraphList.Count() * CountWeek,
                    StartDate = DateOnlyNow(),
                    Status = "Активен"
                };
                DataBase.GetInstance().Subscriptions.Add(sub);
                DataBase.GetInstance().SaveChanges();

                Subscription last = DataBase.GetInstance().Subscriptions.OrderBy(s => s.Id).Last();

                foreach (Serviceworkersgraph swg in AddGraphList)
                {
                    DataBase.GetInstance().Subscriptionservices.Add(new Subscriptionservice() { IdSubscrirtion = last.Id, IdService = swg.Id });
                }



                DataBase.GetInstance().Sales.Add(new Sale() { IdSubscription = last.Id, IdWorker = Worker.Id, Date = DateOnly.FromDateTime(DateTime.Now), Sum = (decimal)(AddSelectedService.PricePerHour) * AddGraphList.Count() * CountWeek });
                DataBase.GetInstance().SaveChanges();
                MessageBox.Show("Абонемент добавлен");

                AddClient = null;
                AddGraphList = new List<Serviceworkersgraph>();
                AddSelectedService = null;
                AddSelectedWorker = null;
                Price = 0;
                SelectedIndexCombobox = 0;
            }

        }

        private void Exid(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
        private void ExidInSubsc(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new SubscriptionPage(Worker);
        }
        public void ShowButton(bool istrue)
        {
            if (istrue)
            {
                ButtonExitInMain.Visibility = Visibility.Visible;
                ButtonExitInSub.Visibility = Visibility.Collapsed;
            }
            else
            {
                ButtonExitInMain.Visibility = Visibility.Collapsed;
                ButtonExitInSub.Visibility = Visibility.Visible;
            }
        }

        private void DeleteService(object sender, RoutedEventArgs e)
        {
            AddSelectedService = null;
        }

        private void DeleteWorker(object sender, RoutedEventArgs e)
        {
            AddSelectedWorker = null;
        }

        private void DeleteGraph()
        {
            AddGraphList = new List<Serviceworkersgraph>();
            Signal(nameof(AddGraphList));
        }
    }
}
