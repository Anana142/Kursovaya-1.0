using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya_1._0;

public partial class Service
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? IdBranch { get; set; }

    public decimal? PricePerHour { get; set; }

    public int? NumberOfPersons { get; set; }

    public string? Description { get; set; }

    public virtual Branch? IdBranchNavigation { get; set; }

    public virtual ICollection<Serviceworkersgraph> Serviceworkersgraphs { get; } = new List<Serviceworkersgraph>();

    
}

