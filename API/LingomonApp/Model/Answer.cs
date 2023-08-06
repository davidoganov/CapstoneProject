using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Answer
{
    public int Questionid { get; set; }

    public string Correct { get; set; } = null!;

    public string Wrong1 { get; set; } = null!;

    public string Wrong2 { get; set; } = null!;

    public string Wrong3 { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
