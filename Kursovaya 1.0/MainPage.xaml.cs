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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private Subscription selected;

        public DataBase DataBase { get; set; } = new DataBase();
        public List<Subscription> ListSubscriptions { get; set; }


        public string servtitle { get; set; }
        public string ServiceTitle { get; set; }
        public List<Graph> GraphicsView { get; set; } 
        public int UsedVisit { get; set; }
        
        
        public Subscription Selected { get => selected; set { selected = value; Signal(); } }
        public MainPage()
        {
            InitializeComponent();

            this.ListSubscriptions = DataBase.GetInstance().Subscriptions.Include(s => s.IdClientNavigation).Include(s => s.IdPeriodNavigation).Include(s => s.IdServices).Include(s => s.Attendances).Include(s => s.IdDiscountNavigation).ToList();
           

             DataContext = this;



        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
