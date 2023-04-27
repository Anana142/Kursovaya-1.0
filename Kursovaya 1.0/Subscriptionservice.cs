using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Subscriptionservice
{
    public int IdService { get; set; }

    public int IdSubscrirtion { get; set; }

    public int Id { get; set; }

    public virtual Serviceworkersgraph IdServiceNavigation { get; set; } = null!;

    public virtual Subscription IdSubscrirtionNavigation { get; set; } = null!;
}
