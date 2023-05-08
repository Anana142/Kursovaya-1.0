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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace Kursovaya_1._0
{
    /// <summary>
    /// Логика взаимодействия для StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public ISeries[] SeriesCount { get; set; }
        public ISeries[] SeriesPrice { get; set; }

        public List<string> ServiceTitle { get; set; }   
        public List<DataServiceDiagram> DiagramData { get; set; } = new List<DataServiceDiagram>();

        Worker Worker;  
        public StatisticsPage(Worker worker)
        {

            InitializeComponent();
            List<string> colors = new List<string> { "#826AED", "#ffc2e2", "#c37df8", "#caadff", "#ab51e3", "#acdcff", "#dc93f6" };
            ServiceTitle = DataBase.GetInstance().Services.Where(s => s.IsDeleted != true).Select(s => s.Title).ToList();

            List<Sale> Sales = DataBase.GetInstance().Sales.Include(s => s.IdSubscriptionNavigation).ToList();

            if (ServiceTitle != null && ServiceTitle.Count > 0)
            {

                SeriesCount = new ISeries[ServiceTitle.Count];
                SeriesPrice = new ISeries[ServiceTitle.Count];


                for (int i = 0; i < ServiceTitle.Count; i++) 
                {

                    List<Sale> sales = Sales.Where(s => s.IdSubscriptionNavigation.ServiceTitle == ServiceTitle[i]).ToList();

                    int Count = sales.Count;
                    double Price = (double)sales.Sum(s => s.Sum);

                    if (Count > 0)
                    {
                        SeriesCount[i] = new PieSeries<int>
                        {
                            Name = ServiceTitle[i],
                            Values = new[] { Count },
                            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                            DataLabelsSize = 15,
                            DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                            DataLabelsFormatter = point => point.PrimaryValue.ToString(),
                            Fill = new SolidColorPaint(SKColor.Parse(colors[i])),
                            InnerRadius = 80   
                        };
                    }
                    else
                    {
                        SeriesCount[i] = new PieSeries<int>
                        {
                            Name = ServiceTitle[i],
                            Values = new[] { Count },
                            
                        };
                    }
                    if (Price > 0)
                    {
                        SeriesPrice[i] = new PieSeries<double>
                        {
                            Name = ServiceTitle[i],
                            Values = new[] { Price },
                            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                            DataLabelsSize = 15,
                            DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                            DataLabelsFormatter = point => point.PrimaryValue.ToString("N2") + "руб.",
                            Fill = new SolidColorPaint(SKColor.Parse(colors[i])),
                            InnerRadius = 80
                        };
                    }
                    else
                    {
                        SeriesPrice[i] = new PieSeries<double>
                        {
                            Name = ServiceTitle[i],
                            Values = new[] { Price },
                        };
                    }
                }

            }

            Worker = worker;
            DataContext = this;


        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
