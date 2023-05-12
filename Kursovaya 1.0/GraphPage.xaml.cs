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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для GraphPage.xaml
    /// </summary>
    public partial class GraphPage : Page, INotifyPropertyChanged
    {
        private List<Graph> listGrapics;
        private Service selectedService;
        private Worker selectedWorker;
        private Graph selectedGraphic;

        public Worker Worker { get; set; }

        public List<Serviceworkersgraph> ListGraph { get; set; }
        public Serviceworkersgraph SelectedGraph { get; set; }


        public List<Service> ListService { get; set; }
        public Service SelectedService { get => selectedService; set { selectedService = value; FillGraphicsList(); } }
        public Service AddSelectedService { get; set; }

        public List<Worker> ListWorker { get; set; }
        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; FillGraphicsList(); } }
        public Worker AddSelectedWorker { get; set; }

        public List<Graph> ListGrapics { get { return listGrapics; } set => listGrapics = value; }
        public Graph SelectedGraphic { get => selectedGraphic; set { selectedGraphic = value; Signal(); } }
        public Graph AddSelectedSwg { get; set; }

        public GraphPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;
            ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                                   .Include(s => s.IdServiceNavigation)
                                                                   .Include(s => s.IdGraphNavigation)
                                                                   .Where(s => s.IsDeleted != true)
                                                                   .OrderBy(s => s.IdServiceNavigation.Title).ThenBy(s => s.IdGraphNavigation).ToList();


            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).ToList();
            ListWorker = DataBase.GetInstance().Workers.Where(s => s.IsDeleted != true && s.IdPost == 2).ToList();




            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
        private void FillGraphicsList()
        {
            if (SelectedWorker != null && SelectedService != null)
            {
                List<Graph> graphs = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdGraphNavigation).Where(s => s.IsDeleted != true && s.IdWorker == SelectedWorker.Id).Select(s => s.IdGraphNavigation).ToList();

                List<Graph> FreeGraphs = DataBase.GetInstance().Graphs.ToList();

                foreach (Graph graph in graphs)
                {
                    FreeGraphs.Remove(graph);
                }

                ListGrapics = FreeGraphs;
                Signal(nameof(ListGrapics));
            }


        }

        private void AddListGraph(object sender, MouseButtonEventArgs e)
        {
            FillGraphicsList();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (SelectedWorker != null && SelectedService != null && SelectedGraphic != null)
            {
                Serviceworkersgraph swg = new Serviceworkersgraph { IdService = SelectedService.Id, IdGraph = SelectedGraphic.Id, IdWorker = SelectedWorker.Id };

                DataBase.GetInstance().Serviceworkersgraphs.Add(swg);
                DataBase.GetInstance().SaveChanges();

                ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                                  .Include(s => s.IdServiceNavigation)
                                                                  .Include(s => s.IdGraphNavigation)
                                                                  .Where(s => s.IsDeleted != true)
                                                                  .OrderBy(s => s.IdServiceNavigation.Title)
                                                                  .ThenBy(s => s.IdGraphNavigation).ToList();

                Signal(nameof(ListGraph));

                SelectedGraphic = new Graph();
                SelectedWorker = new Worker();
                SelectedService = new Service();
            }

        }

        private void OpenPanel(object sender, RoutedEventArgs e)
        {
            GraphPanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 430;
            da.Duration = TimeSpan.FromMilliseconds(300);

            GraphPanel.BeginAnimation(DockPanel.WidthProperty, da);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedGraph != null)
            {
                SelectedGraph.IsDeleted = true;
                DataBase.GetInstance().Serviceworkersgraphs.Update(SelectedGraph);
                DataBase.GetInstance().SaveChanges();

                ListGraph = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdWorkerNavigation)
                                                                  .Include(s => s.IdServiceNavigation)
                                                                  .Include(s => s.IdGraphNavigation)
                                                                  .Where(s => s.IsDeleted != true)
                                                                  .OrderBy(s => s.IdServiceNavigation.Title)
                                                                  .ThenBy(s => s.IdGraphNavigation).ToList();

                Signal(nameof(ListGraph));
            }
        }

        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = GraphPanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            GraphPanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            GraphPanel.Visibility = Visibility.Collapsed;
        }
    }
}
