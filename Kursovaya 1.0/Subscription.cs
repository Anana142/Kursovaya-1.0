using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Kursovaya_1._0;

public partial class Subscription
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? IdPeriod { get; set; }

    public int? IdClient { get; set; }

    public int TotalVisits { get; set; }

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Period? IdPeriodNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public virtual ICollection<Subscriptionservice> Subscriptionservices { get; } = new List<Subscriptionservice>();

    [NotMapped]

    public List<Graph> GraphicsView
    {
        get
        {
            List<Graph> graphs = new List<Graph>();

            foreach (var SS in Subscriptionservices)
            {
                graphs.Add(DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdGraphNavigation).FirstOrDefault(s => s.Id == SS.IdService).IdGraphNavigation);
            }

            return graphs;
        }
    }
    [NotMapped]
    public string ServiceTitle
    {
        get
        {
            Serviceworkersgraph sqg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdServiceNavigation).FirstOrDefault(s => s.Id == Subscriptionservices.First().IdService);

            string title = sqg.IdServiceNavigation.Title;

            return title;
        }
    }
    [NotMapped]
    public int UsedVisits
    {
        get
        {
            return TotalVisits - Attendances.Count;
        }
    }
    [NotMapped]
    public string Price
    {
        get
        {
            decimal? price = 0;
            if (DataBase.GetInstance().Sales.FirstOrDefault(s => s.IdSubscription == this.Id) != null)
                price = DataBase.GetInstance().Sales.FirstOrDefault(s => s.IdSubscription == this.Id).Sum;
            return price.ToString() + " руб.";
        }
    }
    [NotMapped]
    public string DataStartView
    {
        get
        {
            return this.StartDate.ToString();
        }
    }

}
