using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Period
{
    public int Id { get; set; }

    public int Duration { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
