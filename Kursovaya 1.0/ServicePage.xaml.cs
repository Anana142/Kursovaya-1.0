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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page, INotifyPropertyChanged
    {
        private Service selectedService;
        private List<Service> listServise;
        private Service editServise;

        Worker Worker { get; set; }
        public Service SelectedService { get => selectedService; set { selectedService = value; Signal(); } }
        public Service EditServise { get => editServise; set { editServise = value; Signal(); } }
        public List<Service> ListService { get => listServise; set { listServise = value; Signal(); } }

        public ServicePage(Worker worker)
        {
            InitializeComponent();
            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).ToList();
            Worker = worker;
            EditServise = new Service();
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

        private void OpenNewServicePanel(object sender, RoutedEventArgs e)
        {
            EditServise = new Service();

            AddServicePanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 400;
            da.Duration = TimeSpan.FromMilliseconds(300);

            AddServicePanel.BeginAnimation(DockPanel.WidthProperty, da);

        }

        private void SaveNewServiceInServiceList(object sender, RoutedEventArgs e)
        {
            
                if (EditServise != null)
                {
                    if (EditServise.Id == 0 && EditServise.Title != null)
                    {

                        DataBase.GetInstance().Services.Add(EditServise);
                        DataBase.GetInstance().SaveChanges();

                        MessageBox.Show("Услуга добавлена");
                    }
                    else
                    {
                        if (SelectedService != null && SelectedService.Id != 0)
                        {
                           
                            SelectedService.Title = EditServise.Title;
                            SelectedService.PricePerHour = EditServise.PricePerHour;
                            SelectedService.NumberOfPersons = EditServise.NumberOfPersons;
                            SelectedService.Description = EditServise.Description;

                            DataBase.GetInstance().Services.Update(SelectedService);
                            DataBase.GetInstance().SaveChanges();
                            
                        }
                    }

                    ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).ToList();

                    EditServise = new Service();
                    
                    Signal(nameof(EditServise));

                }
                else
                    MessageBox.Show("Ошибка :(");


            ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).ToList();
        }

        private void DeleteService(object sender, RoutedEventArgs e)
        {
            if (SelectedService != null && SelectedService.Id != 0 && (bool)new YesNoWindow("Удалить запись?").ShowDialog())
            {
                SelectedService.IsDeleted = true;
                DataBase.GetInstance().Update(SelectedService);
                DataBase.GetInstance().SaveChanges();

                DoubleAnimation da = new DoubleAnimation();
                da.From = AddServicePanel.ActualWidth;
                da.To = 30;
                da.Duration = TimeSpan.FromMilliseconds(300);
                da.Completed += CloseEnd;
                AddServicePanel.BeginAnimation(DockPanel.WidthProperty, da);

                ListService = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).ToList();

            }
        }

        private void EditNewServicePanel(object sender, RoutedEventArgs e)
        {
            if (SelectedService != null && SelectedService.Id != 0)
            {
                EditServise.Id = SelectedService.Id;
                EditServise.Title = SelectedService.Title;
                EditServise.PricePerHour = SelectedService.PricePerHour;
                EditServise.NumberOfPersons = SelectedService.NumberOfPersons;
                EditServise.Description = SelectedService.Description;


                AddServicePanel.Visibility = Visibility.Visible;

                DoubleAnimation da = new DoubleAnimation();
                da.From = 30;
                da.To = 400;
                da.Duration = TimeSpan.FromMilliseconds(300);

                AddServicePanel.BeginAnimation(DockPanel.WidthProperty, da);

                Signal(nameof(EditServise));
            }
          
                

        }

        private void CloseNewServicePanel(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = AddServicePanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            AddServicePanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            AddServicePanel.Visibility = Visibility.Collapsed;
        }
    }
}
