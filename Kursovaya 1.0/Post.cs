using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Post
{
    public int Id { get; set; }

    public string? Post1 { get; set; }

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();
}
