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
    /// Логика взаимодействия для TrainerPage.xaml
    /// </summary>
    public partial class TrainerPage : Page, INotifyPropertyChanged
    {
        public Worker Worker { get; set; }

        private List<Worker> listWorker;
        private Worker selectedWorker;
        private string workerBirthDay = "";

        public Worker SelectedWorker { get => selectedWorker; set { selectedWorker = value; Signal(); } }
        public List<Worker> ListWorker { get => listWorker; set { listWorker = value; Signal(); } }
        public string WorkerBirthDay { get => workerBirthDay; set { workerBirthDay = value; SpelledCorrectly(); Signal(); } }
        public Worker EditWorker { get; set; } = new Worker();
        public TrainerPage(Worker worker)
        {
            InitializeComponent();

            ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && (s.IsDeleted == null || s.IsDeleted == false)).ToList();
            Worker = worker;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenNewWorkerPanel(object sender, RoutedEventArgs e)
        {
            AddWorkerPanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 400;
            da.Duration = TimeSpan.FromMilliseconds(300);

            AddWorkerPanel.BeginAnimation(DockPanel.WidthProperty, da);
        }

        private void EditNewWorkerPanel(object sender, RoutedEventArgs e)
        {
            if (SelectedWorker != null)
            {
                EditWorker.Id = SelectedWorker.Id;
                EditWorker.Surname = SelectedWorker.Surname;
                EditWorker.Name = SelectedWorker.Name;
                EditWorker.Patronymic = SelectedWorker.Patronymic;
                EditWorker.Birthday = SelectedWorker.Birthday;
                WorkerBirthDay = SelectedWorker.Birthday.ToString();
                EditWorker.PhoneNumber = SelectedWorker.PhoneNumber;
                EditWorker.Gender = SelectedWorker.Gender;
                EditWorker.PassportDetails = SelectedWorker.PassportDetails;
                EditWorker.Street = SelectedWorker.Street;
                EditWorker.HomeNumber = SelectedWorker.HomeNumber;
                EditWorker.HomeNumber = SelectedWorker.HomeNumber;
                EditWorker.FlatNumber = SelectedWorker.FlatNumber;
                EditWorker.Email = SelectedWorker.Email;
                EditWorker.IsDeleted = SelectedWorker.IsDeleted;

                AddWorkerPanel.Visibility = Visibility.Visible;

                DoubleAnimation da = new DoubleAnimation();
                da.From = 30;
                da.To = 400;
                da.Duration = TimeSpan.FromMilliseconds(300);

                AddWorkerPanel.BeginAnimation(DockPanel.WidthProperty, da);

                Signal(nameof(EditWorker));
            }
        }

        private void Deleteworker(object sender, RoutedEventArgs e)
        {
            if (SelectedWorker != null && SelectedWorker.Id > 0)
            {
                SelectedWorker.IsDeleted = true;
                DataBase.GetInstance().Workers.Update(SelectedWorker);
                DataBase.GetInstance().SaveChanges();
            }

            ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && s.IsDeleted != true && s.Id != SelectedWorker.Id).ToList();
            
            WorkerGrid.Items.Refresh();
        }
              

        private void CloseNewWorkerPanel(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = AddWorkerPanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            AddWorkerPanel.BeginAnimation(DockPanel.WidthProperty, da);

        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            AddWorkerPanel.Visibility = Visibility.Collapsed;
        }

        private void SaveNewWorkerInWorkerList(object sender, RoutedEventArgs e)
        {
            if (EditWorker != null && WorkerBirthDay != "")
            {
                EditWorker.Birthday = DateOnly.Parse(WorkerBirthDay);
                if (EditWorker.Id == 0 && EditWorker.Name != null)
                {
                    EditWorker.IdPost = 2;
                    EditWorker.IsDeleted = false;

                    DataBase.GetInstance().Workers.Add(EditWorker);
                    DataBase.GetInstance().SaveChanges();

                    MessageBox.Show("Сотрудник добавлен");
                }
                else
                {
                    if (EditWorker.Name != null)
                    {
                        SelectedWorker.Id = EditWorker.Id;
                        SelectedWorker.Surname = EditWorker.Surname;
                        SelectedWorker.Name = EditWorker.Name;
                        SelectedWorker.Patronymic = EditWorker.Patronymic;
                        SelectedWorker.Birthday = EditWorker.Birthday;
                        SelectedWorker.PhoneNumber = EditWorker.PhoneNumber;
                        SelectedWorker.Gender = EditWorker.Gender;
                        SelectedWorker.PassportDetails = EditWorker.PassportDetails;
                        SelectedWorker.Street = EditWorker.Street;
                        SelectedWorker.HomeNumber = EditWorker.HomeNumber;
                        SelectedWorker.FlatNumber = EditWorker.FlatNumber;
                        SelectedWorker.Email = EditWorker.Email;

                        DataBase.GetInstance().Workers.Update(SelectedWorker);
                        DataBase.GetInstance().SaveChanges();

                    }
                    else
                    {
                        MessageBox.Show("Ошибка :)");
                    }
                    
                }
                
                ListWorker = DataBase.GetInstance().Workers.Where(s => s.IdPost == 2 && (s.IsDeleted == null || s.IsDeleted == false)).ToList();

                EditWorker = new Worker();
                WorkerBirthDay = "";
                Signal(nameof(EditWorker));
                Signal(nameof(WorkerBirthDay));
            }
            else
                MessageBox.Show("Ошибка :(");
            WorkerBirthDay = "";
        }

        private void SpelledCorrectly()
        {
            DateOnly data;

            if (DateOnly.TryParse(WorkerBirthDay, out data))
            {
                Birthday.Foreground = new SolidColorBrush(Colors.Black);
                SaveNewWorker.IsEnabled = true;
            }
            else
            {
                Birthday.Foreground = new SolidColorBrush(Colors.Red);
                SaveNewWorker.IsEnabled = false;
            }
        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
