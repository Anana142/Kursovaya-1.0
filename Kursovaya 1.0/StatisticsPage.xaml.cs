using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
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

        public ObservableCollection<ISeries> Series { get; set; }
        public List<Axis> YAxes { get; set; }
        public List<Axis> XAxes { get; set; }

        public List<string> ServiceTitle { get; set; }

        public List<Sale> SaleList { get; set; } 
        public List<int> Indices { get; set; } =  new List<int> ();
        
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


            // График продаж за день

            List<DateOnly> dateTimes = DataBase.GetInstance().Sales.Select(s => s.Date).Distinct().ToList();
           
           
            foreach (var date in dateTimes)
            {
                List<Sale> sl = DataBase.GetInstance().Sales.Where(s => s.Date == date).ToList();
                Indices.Add(sl.Count);
            }

            string[] DateValue = new string[dateTimes.Count];

            for(int i = 0; i < dateTimes.Count; i++) {
                DateValue[i] = dateTimes[i].ToString();
            }

            Series = new ObservableCollection<ISeries>
                {
                    new ColumnSeries<int>
                    {
                        Values = Indices,
                        Fill = new SolidColorPaint(SKColors.White),
                        TooltipLabelFormatter =
                        (chartPoint) => $" {chartPoint.PrimaryValue}"


                    }
                };
            XAxes = new List<Axis>
{
                new Axis
                        {
                            Labels = DateValue,
                            LabelsPaint = new SolidColorPaint(SKColors.White)

                        }
                    };

            YAxes = new List<Axis> {

                new Axis {
                    Labeler = Labelers.Default,
                    LabelsPaint = new SolidColorPaint(SKColors.White)
                 }
            };





            Worker = worker;
            DataContext = this;


        }

        private void OpenMainPage(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new MainPage(Worker);
        }
    }
}
