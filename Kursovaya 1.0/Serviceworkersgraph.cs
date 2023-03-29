using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Serviceworkersgraph
{
    public int Id { get; set; }

    public int? IdService { get; set; }

    public int? IdWorker { get; set; }

    public int? IdGraph { get; set; }

    public virtual Graph? IdGraphNavigation { get; set; }

    public virtual Service? IdServiceNavigation { get; set; }

    public virtual Worker? IdWorkerNavigation { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
