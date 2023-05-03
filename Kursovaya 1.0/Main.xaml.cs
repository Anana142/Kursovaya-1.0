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
using System.Windows.Shapes;

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window, INotifyPropertyChanged
    {
        public Worker Worker { get; set; }

        public Main(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            Navigation.GetInstance().CurrentPage = new MainPage(worker);

            DataContext = Navigation.GetInstance();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void OpenMenuPanel(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 300;
            da.Duration = TimeSpan.FromMilliseconds(300);

            MenuPanel.BeginAnimation(DockPanel.WidthProperty, da);
            ButtonMenuOpen.Visibility = Visibility.Collapsed;
        }

        private void CloseMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = MenuPanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;

            MenuPanel.BeginAnimation(DockPanel.WidthProperty, da);

            ButtonMenuOpen.Visibility = Visibility.Visible;
            Signal();
            
        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;

            ButtonMenuOpen.Visibility = Visibility.Visible;
            Signal();
            
        }

        private void SubscriptionPageOpen(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new SubscriptionPage(Worker);

            CloseMenu(ButtonMenuOpen, e);
        }

        private void GraphPageOpen(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new GraphPage(Worker);

            CloseMenu(ButtonMenuOpen, e);
        }

        private void ClientPageOpen(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ClientPage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenworkerPage(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new WorkerPage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenSalePage(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new SalePage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenTrainerPage(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new TrainerPage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenServicePage(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ServicePage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenTrashPage(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new DeletePage(Worker);
            CloseMenu(ButtonMenuOpen, e);
        }

        private void OpenMainWindow(object sender, MouseButtonEventArgs e)
        {
            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
