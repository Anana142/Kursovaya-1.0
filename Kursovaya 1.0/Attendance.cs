using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Attendance
{
    public int Id { get; set; }

    public int? IdSubscription { get; set; }

    public DateTime? Date { get; set; }

    public virtual Subscription? IdSubscriptionNavigation { get; set; }
}
