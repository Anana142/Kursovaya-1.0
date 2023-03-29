using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Branch
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
