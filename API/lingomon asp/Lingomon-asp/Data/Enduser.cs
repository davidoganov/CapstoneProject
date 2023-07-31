using System;
using System.Collections.Generic;

namespace Lingomon_asp.Data;

public partial class Enduser
{
    public string Id { get; set; } = null!;

    public int Classid { get; set; }

    public int? Lingomon { get; set; }

    public string Password { get; set; } = null!;

    public decimal? Spelling { get; set; }

    public decimal? Grammar { get; set; }

    public decimal? Diction { get; set; }

    public decimal? Conjugation { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Lingomon? LingomonNavigation { get; set; }
}
