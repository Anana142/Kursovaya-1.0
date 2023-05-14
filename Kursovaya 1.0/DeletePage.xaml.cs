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
    /// Логика взаимодействия для DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page, INotifyPropertyChanged
    {
        private Service selectedService;
        private List<Service> listService;
        private List<Worker> listWorker;
        private Worker selectedWorker;
        private List<Serviceworkersgraph> listGraph;
        private Serviceworkersgraph selectedGraph;

        public Service SelectedService { get => selectedService; set { selectedService = value; Signal();  } }
        public List<Service> ListService { get => listService; set { listService = value; Signal(); } }

        public List<Worker> ListWorker { get => listWorker; set { listWorker = value; Signal(); } }
        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; Signal();  } }

        public List<Serviceworkersgraph> ListGraph { get => listGraph; set { listGraph = value; Signal(); } }
        public Serviceworkersgraph SelectedGraph { get => selectedGraph; set { selectedGraph = value; Signal(); } }

        DataBase dataBase { get; set; } = new DataBase();   
        Worker Worker { get; set; }

        public DeletePage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted == true).ToList();
            ListWorker = DataBase.GetInstance().Workers.Where(s => s.IsDeleted == true && s.IdPost == 2).ToList();

            ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                                   .Include(s => s.IdServiceNavigation)
                                                                   .Include(s => s.IdGraphNavigation)
                                                                   .Where(s => s.IsDeleted == true)
                                                                   .OrderBy(s => s.IdServiceNavigation.Title).ThenBy(s => s.IdGraphNavigation).ToList();


            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenGraph(object sender, RoutedEventArgs e)
        {
            ServiceButton.BorderThickness = new Thickness(0, 0, 0, 0);
            WorkerButton.BorderThickness = new Thickness(0, 0, 0, 0);
            GraphButton.BorderThickness = new Thickness(0, 0, 0, 1);
            WorkerGrid.Visibility = Visibility.Collapsed;
            ServiceGrid.Visibility = Visibility.Collapsed;
            GridGraph.Visibility = Visibility.Visible;
        }

        private void OpenWorker(object sender, RoutedEventArgs e)
        {
            ServiceButton.BorderThickness = new Thickness(0, 0, 0, 0);
            WorkerButton.BorderThickness = new Thickness(0, 0, 0, 1);
            GraphButton.BorderThickness = new Thickness(0, 0, 0, 0);

            WorkerGrid.Visibility = Visibility.Visible;
            ServiceGrid.Visibility = Visibility.Collapsed;
            GridGraph.Visibility = Visibility.Collapsed;


        }

        private void OpenService(object sender, RoutedEventArgs e)
        {
            ServiceButton.BorderThickness = new Thickness(0, 0, 0, 1);
            WorkerButton.BorderThickness = new Thickness(0, 0, 0, 0);
            GraphButton.BorderThickness = new Thickness(0, 0, 0, 0);

            WorkerGrid.Visibility = Visibility.Collapsed;
            ServiceGrid.Visibility = Visibility.Visible;
            GridGraph.Visibility = Visibility.Collapsed;
        }

        

        private void NotDel(object sender, RoutedEventArgs e)
        {
            if(ServiceGrid.Visibility == Visibility.Visible) {
                if(SelectedService != null)
                {
                    SelectedService.IsDeleted = false;
                    DataBase.GetInstance().Services.Update(SelectedService);
                    DataBase.GetInstance().SaveChanges();
                    ListService = DataBase.GetInstance().Services.Where(s =>  s.IsDeleted == true).ToList();

                }
            }
            else if(WorkerGrid.Visibility == Visibility.Visible)
            {
               if(SelectedWorker != null)
                {
                    SelectedWorker.IsDeleted = false;
                    DataBase.GetInstance().Workers.Update(SelectedWorker);
                    DataBase.GetInstance().SaveChanges();
                    ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost==2 && s.IsDeleted == true).ToList();

                }
            }
            else if(GridGraph.Visibility == Visibility.Visible)
            {
                if(SelectedGraph != null)
                {
                    SelectedGraph.IsDeleted = false;
                    DataBase.GetInstance().Serviceworkersgraphs.Update(SelectedGraph);
                    DataBase.GetInstance().SaveChanges();
                    ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                                   .Include(s => s.IdServiceNavigation)
                                                                   .Include(s => s.IdGraphNavigation)
                                                                   .Where(s => s.IsDeleted == true)
                                                                   .OrderBy(s => s.IdServiceNavigation.Title).ThenBy(s => s.IdGraphNavigation).ToList();
                }


            }
        }
        private void DeleteService() {
            


            if (ListService.Count != 0)
            {

                foreach (var service in ListService)
                {

                    List<Serviceworkersgraph> s = DataBase.GetInstance().Serviceworkersgraphs.Where(s => s.IdService == service.Id).ToList();

                    foreach (var item in s)
                    {

                        List<Subscriptionservice> ss = item.Subscriptionservices.ToList();

                        foreach (var i in ss)
                        {
                            List<Subscription> sub = DataBase.GetInstance().Subscriptions.Where(s => s.Id == i.IdSubscrirtion).ToList();

                            if (sub.Count > 0)
                            {
                                foreach (var y in sub)
                                {
                                    SubscriptionExtension.DeleteSubscriotion(y);
                                    DataBase.GetInstance().SaveChanges();

                                }
                            }
                        }

                        if (item.Id != 0)
                        {

                            DataBase.GetInstance().Serviceworkersgraphs.Remove(item);
                            DataBase.GetInstance().SaveChanges();
                        }

                    }
                }


                foreach (var item in ListService)
                {

                    DataBase.GetInstance().Services.Remove(item);
                    DataBase.GetInstance().SaveChanges();

                }

            }

            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted == true).ToList();
        }
        private void DeleteWorker()
        {
            if (ListWorker.Count != 0)
            {
                foreach (var worker in ListWorker)
                {

                    List<Serviceworkersgraph> s = DataBase.GetInstance().Serviceworkersgraphs.Where(s => s.IdWorker == worker.Id).ToList();

                    foreach (var item in s)
                    {

                        List<Subscriptionservice> ss = item.Subscriptionservices.ToList();

                        foreach (var i in ss)
                        {
                            List<Subscription> sub = DataBase.GetInstance().Subscriptions.Where(s => s.Id == i.IdSubscrirtion).ToList();

                            if (sub.Count > 0)
                            {
                                foreach (var y in sub)
                                {
                                    SubscriptionExtension.DeleteSubscriotion(y);
                                    DataBase.GetInstance().SaveChanges();

                                }
                            }

                        }

                        if (item.Id != 0)
                        {
                            DataBase.GetInstance().Serviceworkersgraphs.Remove(item);
                            DataBase.GetInstance().SaveChanges();
                        }



                    }

                }
                foreach (var item in ListWorker)
                {

                    DataBase.GetInstance().Workers.Remove(item);
                    DataBase.GetInstance().SaveChanges();

                }

                ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && s.IsDeleted == true).ToList();
            }
        }
        private void DeleteGraph()
        {
            if (ListGraph.Count != 0)
            {
                foreach (var item in ListGraph)
                {

                    List<Subscriptionservice> ss = item.Subscriptionservices.ToList();

                    foreach (var i in ss)
                    {
                        List<Subscription> sub = DataBase.GetInstance().Subscriptions.Where(s => s.Id == i.IdSubscrirtion).ToList();

                        if (sub.Count > 0)
                        {
                            foreach (var y in sub)
                            {
                                SubscriptionExtension.DeleteSubscriotion(y);
                                DataBase.GetInstance().SaveChanges();

                            }
                        }

                    }

                    if (item.Id != 0)
                    {
                        DataBase.GetInstance().Serviceworkersgraphs.Remove(item);
                        DataBase.GetInstance().SaveChanges();
                    }
                }
                ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                            .Include(s => s.IdServiceNavigation)
                                                            .Include(s => s.IdGraphNavigation)
                                                            .Where(s => s.IsDeleted == true)
                                                            .OrderBy(s => s.IdServiceNavigation.Title).ThenBy(s => s.IdGraphNavigation).ToList();
                Signal(nameof(ListGraph));

            }
        }

        private void DelAll(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.Visibility == Visibility.Visible)
            {
                if (SelectedService != null && SelectedService.Id != 0)
                    ListService = new List<Service> { SelectedService };
                if (ListService.Count > 1) {
                    if((bool)new YesNoWindow("Удалить все?").ShowDialog())
                        DeleteService();
                } 
                    
                else if (ListService.Count == 1)
                {
                    if ((bool)new YesNoWindow("Удалить запись?").ShowDialog())
                        DeleteService();
                }
                
            }
                
                else if (WorkerGrid.Visibility == Visibility.Visible)
                {

                if (SelectedWorker != null && SelectedWorker.Id != 0)
                {
                    ListWorker = new List<Worker> { SelectedWorker };
                }
                if (ListWorker.Count > 1)
                {
                    if ((bool)new YesNoWindow("Удалить все?").ShowDialog())
                        DeleteWorker();
                }

                else if (ListWorker.Count == 1)
                {
                    if ((bool)new YesNoWindow("Удалить запись?").ShowDialog())
                        DeleteWorker();
                }

            }
            else if (GridGraph.Visibility == Visibility.Visible)
            {
                if (SelectedGraph != null && SelectedGraph.Id != 0)
                    ListGraph = new List<Serviceworkersgraph> { SelectedGraph };
                if (ListGraph.Count > 1)
                {
                    if ((bool)new YesNoWindow("Удалить все?").ShowDialog())
                        DeleteGraph();
                }

                else if (ListGraph.Count == 1)
                {
                    if ((bool)new YesNoWindow("Удалить запись?").ShowDialog())
                        DeleteGraph();
                }


            }

        }
        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
