using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursovaya_1._0;

public partial class Worker
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int IdPost { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Gender { get; set; }

    public string? PassportDetails { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Street { get; set; }

    public string? HomeNumber { get; set; }

    public int? FlatNumber { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public virtual ICollection<Serviceworkersgraph> Serviceworkersgraphs { get; } = new List<Serviceworkersgraph>();

    [NotMapped]
    public string FIO { get
        {
            return Surname + " " + Name + " " + Patronymic;
        } }
}
