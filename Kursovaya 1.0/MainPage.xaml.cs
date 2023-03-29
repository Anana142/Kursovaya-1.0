using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public DataBase DataBase { get; set; } = new DataBase();    
        public List<Subscription> ListSubscriptions { get; set; }
        public MainPage()
        {
            InitializeComponent();

            this.ListSubscriptions = DataBase.GetInstance().Subscriptions.ToList();

            DataContext = this;
        }
    }
}
