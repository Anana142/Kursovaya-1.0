using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Client
{
    public int Id { get; set; }

    public string? SurName { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
