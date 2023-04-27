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
using Kursovaya_1; 

namespace Kursovaya_1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public DataBase database { get; set; }  = new DataBase();
        public string Password { get; set; }
        public string Login { get; set; }   



        public MainWindow()
        {
            InitializeComponent();

           

                DataContext = this;
            }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PasswordEdit(object sender, RoutedEventArgs e)
        {
            if(PassBox.Password.Length == 0)
            {
                PlaceholderPassLabel.Visibility = Visibility.Visible;
            }
            else if(PassBox.Password.Length != 0)
                PlaceholderPassLabel.Visibility = Visibility.Hidden;
        }

        private void PasswordVisible(object sender, MouseButtonEventArgs e)
        {
            PlaceholderPassLabel.Visibility = Visibility.Hidden;
            PassBox.Visibility = Visibility.Visible;
            PassBox.Focus();
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Password = PassBox.Password;
            Worker worcer = database.Authorization(Login, Password);

            if (worcer != null)
            {
                Main main = new Main(worcer);
                main.Show();
                this.Close();
            }
            else
            {
                Incorrect.Visibility = Visibility.Visible;    
            }
        }
    }
    }
