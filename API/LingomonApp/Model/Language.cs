using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Language
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
