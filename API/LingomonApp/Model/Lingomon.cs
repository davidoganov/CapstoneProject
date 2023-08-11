using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Lingomon
{
    public int Id { get; set; }

    public int Dexid { get; set; }

    public virtual Lingodex Dex { get; set; } = null!;

    public virtual ICollection<EndUser> Endusers { get; set; } = new List<EndUser>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
