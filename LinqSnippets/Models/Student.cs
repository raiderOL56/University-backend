using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqSnippets.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Grade { get; set; } = 0;
        public bool Certified { get; set; }
    }
}