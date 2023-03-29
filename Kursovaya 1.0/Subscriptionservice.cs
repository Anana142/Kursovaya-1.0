using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Subscriptionservice
{
    public int? IdService { get; set; }

    public int? IdSubscrirtion { get; set; }

    public virtual Serviceworkersgraph? IdServiceNavigation { get; set; }

    public virtual Subscription? IdSubscrirtionNavigation { get; set; }
}
