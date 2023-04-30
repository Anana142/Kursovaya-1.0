using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

    [NotMapped]
    public string ClientName
    {
        get
        {
            Subscription subscription = DataBase.GetInstance().Subscriptions.FirstOrDefault(s => s.Id == this.IdSubscription);
            if(subscription != null)
            {
                Client client = DataBase.GetInstance().Clients.FirstOrDefault(s => s.Id == subscription.IdClient);
                if (client != null)
                    return client.SurName;
            }

            return "";
        }
    }
}
