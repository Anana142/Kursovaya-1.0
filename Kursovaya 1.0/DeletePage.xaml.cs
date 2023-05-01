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

        public Service SelectedService { get => selectedService; set { selectedService = value; Signal(); ButttonVisibility(); } }
        public List<Service> ListService { get => listService; set { listService = value; Signal(); } }

        public List<Worker> ListWorker { get => listWorker; set { listWorker = value; Signal(); } }
        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; Signal(); ButttonVisibility(); } }

        public List<Serviceworkersgraph> ListGraph { get => listGraph; set { listGraph = value; Signal(); } }
        public Serviceworkersgraph SelectedGraph { get => selectedGraph; set { selectedGraph = value; Signal(); } }


        Worker Worker { get; set; }

        public DeletePage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted == true).ToList();
            ListWorker = DataBase.GetInstance().Workers.Where(s => s.IsDeleted == true && s.IdPost == 2).ToList();


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
        }

        private void OpenWorker(object sender, RoutedEventArgs e)
        {
            ServiceButton.BorderThickness = new Thickness(0, 0, 0, 0);
            WorkerButton.BorderThickness = new Thickness(0, 0, 0, 1);
            GraphButton.BorderThickness = new Thickness(0, 0, 0, 0);

            WorkerGrid.Visibility = Visibility.Visible;
            ServiceGrid.Visibility = Visibility.Collapsed;
        }

        private void OpenService(object sender, RoutedEventArgs e)
        {
            ServiceButton.BorderThickness = new Thickness(0, 0, 0, 1);
            WorkerButton.BorderThickness = new Thickness(0, 0, 0, 0);
            GraphButton.BorderThickness = new Thickness(0, 0, 0, 0);

            WorkerGrid.Visibility = Visibility.Collapsed;
            ServiceGrid.Visibility = Visibility.Visible;
        }

        private void ButttonVisibility()
        {
            if (SelectedService != null )
            {
                DeleteOne.Visibility = Visibility.Visible;
                DeleteAll.Visibility = Visibility.Collapsed;

            }

            if (SelectedWorker != null)
            {
                DeleteOne.Visibility = Visibility.Visible;
                DeleteAll.Visibility = Visibility.Collapsed;

            }

            else
            {
                DeleteOne.Visibility = Visibility.Collapsed;
                DeleteAll.Visibility = Visibility.Visible;
            }
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
        }

        private void DelAll(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.Visibility == Visibility.Visible)
            {
                if (SelectedService == null && ListService.Count != 0)
                {
                    

                }
            }
            else if (WorkerGrid.Visibility == Visibility.Visible)
            {
                if (SelectedWorker == null && ListWorker.Count != 0 )
                {
                    foreach (var item in ListWorker) 
                    { 
                    
                            DataBase.GetInstance().Workers.Remove(item);
                            DataBase.GetInstance().SaveChanges();

                    }

                    ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && s.IsDeleted == true).ToList();

                }
            }
        }
    }
}
