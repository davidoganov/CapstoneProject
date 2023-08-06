using System;
using System.Collections.Generic;

namespace LingomonApp.Data;

public partial class Type
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lingodex> Lingodices { get; set; } = new List<Lingodex>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
