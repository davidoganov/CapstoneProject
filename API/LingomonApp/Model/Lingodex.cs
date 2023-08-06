using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Lingodex
{
    public int Id { get; set; }

    public int Typeid { get; set; }

    public string Name { get; set; } = null!;

    public int Hp { get; set; }

    public virtual ICollection<Lingomon> Lingomons { get; set; } = new List<Lingomon>();

    public virtual Type Type { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
