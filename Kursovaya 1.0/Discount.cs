using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Discount
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Size { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
