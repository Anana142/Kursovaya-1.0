using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursovaya_1._0;

public partial class Serviceworkersgraph
{
    public int Id { get; set; }

    public int? IdService { get; set; }

    public int? IdWorker { get; set; }

    public int? IdGraph { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Graph? IdGraphNavigation { get; set; }

    public virtual Service? IdServiceNavigation { get; set; }

    public virtual Worker? IdWorkerNavigation { get; set; }

    public virtual ICollection<Subscriptionservice> Subscriptionservices { get; } = new List<Subscriptionservice>();

    [NotMapped]
    public int Busy
    {
        get
        {
            return Subscriptionservices.Count;

        }
    }
}
