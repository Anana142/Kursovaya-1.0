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

        public List<Client> ClientList { get => clientList; set { clientList = value; Signal(); } }
        public Client SelectedClient { get; set; }
        public string ClientName { get => clientName; set { clientName = value; Signal(); } }

        public string SearchText { get => searchText; set { searchText = value; Search(); } }

        public NewSubscription()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Search()
        {
            var result = DataBase.GetInstance().Clients.
                 Where(s => s.SurName.Contains(SearchText) ||
                 s.Name.Contains(SearchText) ||
                 s.Patronymic.Contains(SearchText)
                 );
            ClientList = result.ToList();

            Signal(nameof(ClientList));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientList = DataBase.GetInstance().Clients.ToList();
            NewClientBorder.Visibility = Visibility.Collapsed;
            MyBorder.Visibility = Visibility.Visible;   

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddClientName(object sender, RoutedEventArgs e)
        {
            if(SelectedClient != null) 
                ClientName = SelectedClient.SurName.ToString() + " " + SelectedClient.Name.ToString();
        }

        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            MyBorder.Visibility = Visibility.Collapsed;
            NewClientBorder.Visibility = Visibility.Visible;    
        }
    }
}
