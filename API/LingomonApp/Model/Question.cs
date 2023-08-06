using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Question
{
    public int Id { get; set; }

    public int Typeid { get; set; }

    public string Language { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public int Strength { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Language LanguageNavigation { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;

    public virtual ICollection<Lingodex> Dices { get; set; } = new List<Lingodex>();
}
