using System;
using System.Collections.Generic;

namespace Kursovaya_1._0;

public partial class Graph
{
    public int Id { get; set; }

    public string? DayOfWeek { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Serviceworkersgraph> Serviceworkersgraphs { get; } = new List<Serviceworkersgraph>();

    public override string ToString()
    {
        return DayOfWeek + " " + StartTime.ToString() + "-" + EndTime.ToString();   
    }
}
