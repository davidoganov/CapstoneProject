using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Class
{
    public int Id { get; set; }

    public int Level { get; set; }

    public virtual ICollection<Enduser> Endusers { get; set; } = new List<Enduser>();
}
