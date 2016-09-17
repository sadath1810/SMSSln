using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSMVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Maths { get; set; }
        public int Physics { get; set; }
        public int Chemistry { get; set; }
    }
}