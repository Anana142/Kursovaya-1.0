using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Subscription
{
    public int Id { get; set; }

    public int? IdServiceWorkerGraph { get; set; }

    public string? Status { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? IdPeriod { get; set; }

    public int? IdDiscount { get; set; }

    public int? IdClient { get; set; }

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Discount? IdDiscountNavigation { get; set; }

    public virtual Period? IdPeriodNavigation { get; set; }

    public virtual Serviceworkersgraph? IdServiceWorkerGraphNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
}
