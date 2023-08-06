using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Npc
{
    public int Id { get; set; }

    public int? Lingomon { get; set; }

    public string Name { get; set; } = null!;

    public string? Dialogue { get; set; }

    public virtual Lingomon? LingomonNavigation { get; set; }
}
