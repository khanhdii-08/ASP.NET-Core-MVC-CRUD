using System;
using System.Collections.Generic;

namespace WebApplicationCRUD.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Email { get; set; }
    }
}
