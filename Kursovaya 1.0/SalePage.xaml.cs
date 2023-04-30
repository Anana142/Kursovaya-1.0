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
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page, INotifyPropertyChanged
    {
        private List<Sale> listSale;
        private Sale selectedSale;

        public Worker Worker { get; set; }
        DataBase dataBase { get; set; } = new DataBase();   

        public Sale SelectedSale { get => selectedSale; set { selectedSale = value; Signal(); OpenSalePanel();  } }
        public List<Sale> ListSale { get => listSale; set { listSale = value; Signal(); } }



        public SalePage(Worker worker)
        {
            InitializeComponent();

            ListSale = DataBase.GetInstance().Sales.Include(s => s.IdWorkerNavigation).Include(s => s.IdSubscriptionNavigation).ToList();
            Worker = worker;

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

        private void DeleteSale(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null)
            {
                Subscription sub = DataBase.GetInstance().Subscriptions.FirstOrDefault(s => s.Id == SelectedSale.IdSubscription);

                dataBase.DeleteSubscriotion(sub);   

                ListSale = DataBase.GetInstance().Sales.ToList();
            }
        }

        private void CloseSalePanel(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = SalePanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            SalePanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            SalePanel.Visibility = Visibility.Collapsed;
        }

        public void OpenSalePanel()
        {
            if (SalePanel.Visibility != Visibility.Visible)
            {
                SalePanel.Visibility = Visibility.Visible;

                DoubleAnimation da = new DoubleAnimation();
                da.From = 30;
                da.To = 400;
                da.Duration = TimeSpan.FromMilliseconds(300);

                SalePanel.BeginAnimation(DockPanel.WidthProperty, da);
            }


         }

    }
}
