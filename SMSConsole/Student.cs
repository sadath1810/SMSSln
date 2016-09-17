using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSConsole
{
    class Student
    {
       
        public Student(int StudentId)
        {
            this.StudentId = StudentId;
        }

        public int StudentId { get; private set; }
        public string StudentName { get; set; }
        public int Maths { get; set; }
        public int Physics { get; set; }
        public int Chemistry { get; set; }

        public override string ToString()
        {
            return string.Format("StudentId: {0} -- StudentName: {1} -- Maths: {2} -- Physics: {3} -- Chemistry: {4}", StudentId, StudentName, Maths, Physics, Chemistry);
        }

        public override bool Equals(object obj)
        {
            Student externalObj = (Student) obj;
            return (this.Maths == externalObj.Maths && this.Physics == externalObj.Physics);
        }
    }
}
