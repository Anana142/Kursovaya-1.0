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
    /// Логика взаимодействия для SubscriptionPage.xaml
    /// </summary>
    public partial class SubscriptionPage : Page, INotifyPropertyChanged
    {
        private Subscription selectedSubscription;
        private List<Subscription> listSubscriptions;

        public List<Subscription> ListSubscriptions { get => listSubscriptions; set { listSubscriptions = value; Signal(); } }
        public Subscription SelectedSubscription { get => selectedSubscription; set { selectedSubscription = value; Signal(); } }
        public Worker Worker { get; set; }
        DataBase dataBase { get; set; } = new DataBase();   
        public SubscriptionPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;
            ListSubscriptions = DataBase.GetInstance().Subscriptions.Include(s => s.IdClientNavigation).Include(s => s.IdPeriodNavigation).Include(s => s.Attendances).Include(s => s.Subscriptionservices).ToList();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddSub(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new NewSubscription(Worker, false);
        }

        private void DeleteSub(object sender, RoutedEventArgs e)
        {
            if (SelectedSubscription != null && SelectedSubscription.Id != 0)
            {
                if ((bool)new YesNoWindow("Удалить запись?").ShowDialog())
                {
                    SubscriptionExtension.DeleteSubscriotion(SelectedSubscription);
                    ListSubscriptions = DataBase.GetInstance().Subscriptions.Include(s => s.IdClientNavigation).Include(s => s.IdPeriodNavigation).Include(s => s.Attendances).Include(s => s.Subscriptionservices).ToList();
                }
            }
        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
