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
using System.Windows.Shapes;

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Worker Worker { get; }
        public Main(Worker worker)
        {
            InitializeComponent();

            Navigation.GetInstance().CurrentPage = new MainPage(worker);

            DataContext = Navigation.GetInstance();
        }

        

        
    }
}
