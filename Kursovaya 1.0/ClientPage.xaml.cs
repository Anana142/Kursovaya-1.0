using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page, INotifyPropertyChanged
    {
        private Client selectedClient = new Client();
        private string birthdayNewClient = "";
        private List<Client> listClient;

        public Worker Worker { get; set; }
        DataBase dataBase { get; set; } = new DataBase();

        public List<Client> ListClient { get => listClient; set { listClient = value; Signal(); } }
        public Client SelectedClient { get => selectedClient; set { selectedClient = value; Signal(); } }
        
        public Client EditClient { get; set; } = new Client();


        public string BirthdayNewClient { get => birthdayNewClient; set { birthdayNewClient = value; SpelledCorrectly(); Signal(); } }


        public ClientPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            ListClient = DataBase.GetInstance().Clients.ToList();

            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenNewClientPanel(object sender, RoutedEventArgs e)
        {
            EditClient = new Client();
            BirthdayNewClient = "";
            Signal(nameof(BirthdayNewClient));
            Signal(nameof(EditClient));

            AddClientPanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 400;
            da.Duration = TimeSpan.FromMilliseconds(300);

            AddClientPanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void EditNewClientPanel(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null && SelectedClient.Id != 0)
            {
                EditClient.Id = SelectedClient.Id;
                EditClient.SurName = SelectedClient.SurName;
                EditClient.Name = SelectedClient.Name;
                EditClient.Patronymic = SelectedClient.Patronymic;
                EditClient.Birthday = SelectedClient.Birthday;
                BirthdayNewClient = SelectedClient.Birthday.ToString();
                EditClient.PhoneNumber = SelectedClient.PhoneNumber;
                EditClient.Gender = SelectedClient.Gender;

                AddClientPanel.Visibility = Visibility.Visible;

                DoubleAnimation da = new DoubleAnimation();
                da.From = 30;
                da.To = 400;
                da.Duration = TimeSpan.FromMilliseconds(300);

                AddClientPanel.BeginAnimation(DockPanel.WidthProperty, da);

                Signal(nameof(EditClient));
            }
        }
        private void CloseNewClientPanel(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = AddClientPanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            AddClientPanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            AddClientPanel.Visibility = Visibility.Collapsed;
        }

        private void SaveNewClientInClientList(object sender, RoutedEventArgs e)
        {
            if (EditClient != null && BirthdayNewClient != "")
            {
                EditClient.Birthday = DateOnly.Parse(BirthdayNewClient);
                if (EditClient.Id == 0)
                {
                    DataBase.GetInstance().Clients.Add(EditClient);
                    DataBase.GetInstance().SaveChanges();

                    MessageBox.Show("Клиент добавлен");
                }
                else
                {
                    SelectedClient.Id = EditClient.Id;
                    SelectedClient.SurName = EditClient.SurName;
                    SelectedClient.Name = EditClient.Name;
                    SelectedClient.Patronymic = EditClient.Patronymic;
                    SelectedClient.Birthday = EditClient.Birthday;
                    SelectedClient.PhoneNumber = EditClient.PhoneNumber;
                    SelectedClient.Gender = EditClient.Gender;

                    DataBase.GetInstance().Clients.Update(SelectedClient);
                    DataBase.GetInstance().SaveChanges();
                }

                ListClient = DataBase.GetInstance().Clients.ToList();

                EditClient = new Client();
                BirthdayNewClient = "";
                Signal(nameof(EditClient));
                Signal(nameof(BirthdayNewClient));
            }
            else
                MessageBox.Show("Ошибка :(");
            BirthdayNewClient = "";
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

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null && SelectedClient.Id != 0 && (bool)new YesNoWindow("Удалить запись?").ShowDialog())
            {
                List<Subscription> subscriptions = DataBase.GetInstance().Subscriptions.Where(s => s.IdClient == SelectedClient.Id).ToList();
                if (subscriptions.Count == 0)
                {
                    DataBase.GetInstance().Clients.Remove(SelectedClient);
                    DataBase.GetInstance().SaveChanges();
                }
                else
                {
                    foreach (Subscription sub in subscriptions)
                    {
                       SubscriptionExtension.DeleteSubscriotion(sub);
                    }

                    DataBase.GetInstance().Clients.Remove(SelectedClient);
                    DataBase.GetInstance().SaveChanges();

                }

                ListClient = DataBase.GetInstance().Clients.ToList();
                CloseNewClientPanel(sender, e);
            }

            
        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
