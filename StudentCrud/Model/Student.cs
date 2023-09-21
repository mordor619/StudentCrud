using System;
using System.Collections.Generic;

namespace StudentCrud.Model;

public partial class Student
{
    public string? Id { get; set; } = null;

    public string Name { get; set; } = null!;

    public int Age { get; set; }
}
