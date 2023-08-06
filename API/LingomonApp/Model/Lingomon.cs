using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Lingomon
{
    public int Id { get; set; }

    public int Dexid { get; set; }

    public virtual Lingodex Dex { get; set; } = null!;

    public virtual ICollection<Enduser> Endusers { get; set; } = new List<Enduser>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
