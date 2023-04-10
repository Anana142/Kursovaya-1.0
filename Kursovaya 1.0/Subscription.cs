using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

    public int? IdDiscount { get; set; }

    public int? IdClient { get; set; }

    public int TotalVisits { get; set; }

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Discount? IdDiscountNavigation { get; set; }

    public virtual Period? IdPeriodNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public virtual ICollection<Serviceworkersgraph> IdServices { get; } = new List<Serviceworkersgraph>();

    [NotMapped]
    
    public List<Graph> GraphicsView
    {
        get
        {
            List<Graph> graphs = new List<Graph>();

            foreach (Serviceworkersgraph swg in this.IdServices)
            {
                if (DataBase.GetInstance().Graphs.FirstOrDefault(s => s.Id == swg.IdGraph) != null)
                    graphs.Add(DataBase.GetInstance().Graphs.FirstOrDefault(s => s.Id == swg.IdGraph));
            }

            return graphs;
        }
    }
    
    public string ServiceTitle
    {
        get
        {
            int IdSWG = this.IdServices.First().Id;

            Serviceworkersgraph sqg = DataBase.GetInstance().Serviceworkersgraphs.Include(s => s.IdServiceNavigation).FirstOrDefault(s => s.Id == IdSWG);

            string title = sqg.IdServiceNavigation.Title;

            return title; 
            //return DataBase.GetInstance().Services.FirstOrDefault(s => s.Id == this.IdServices.First().IdService).Title;
        }
    }

    public int UsedVisits
    {
        get
        {
            return TotalVisits - Attendances.Count;
        }
    }

    public string Price
    {
        get
        {

            decimal? price = (decimal)DataBase.GetInstance().Services.FirstOrDefault(s => s.Id == this.IdServices.First().IdService).PricePerHour;

            decimal? discount = (decimal)this.IdDiscountNavigation.Size / 100;

            price = price - (price * discount);

            return price.ToString() + " руб.";
        }
    }
}
