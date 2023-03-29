using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Sale
{
    public int Id { get; set; }

    public int? IdSubscription { get; set; }

    public int? IdWorker { get; set; }

    public decimal? Sum { get; set; }

    public DateTime? Date { get; set; }

    public virtual Subscription? IdSubscriptionNavigation { get; set; }

    public virtual Worker? IdWorkerNavigation { get; set; }
}
