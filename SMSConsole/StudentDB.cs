using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSConsole
{
    class StudentDB
    {
        public StudentDB()
        {
            DSStudent = new DataSet("StudentDB");

            DataTable dtStudent = new DataTable("Student");

            DataColumn dcStudentId = new DataColumn("StudentId", typeof(int));
            dcStudentId.AllowDBNull = false;

            dtStudent.Columns.Add(dcStudentId);


            DataColumn dcStudentName = new DataColumn("StudentName", typeof(string));
            dcStudentName.AllowDBNull = false;
            dtStudent.Columns.Add(dcStudentName);

            //dtStudent.PrimaryKey = new DataColumn[] { dcStudentId};

            DSStudent.Tables.Add(dtStudent);




        }

        public DataSet DSStudent { get; set; }

        public bool AddStudent(Student es)
        {
            DataRow drNew = DSStudent.Tables[0].NewRow();
            drNew[0] = es.StudentId;
            drNew[1] = es.StudentName;

            DSStudent.Tables[0].Rows.Add(drNew);

            return true;
        }

        public Student SelectAStudent(int sId)
        {
            Student es = null;
            
            DataRow[] drows = DSStudent.Tables["Student"].Select("StudentId=" + sId);
            if (drows.Length > 0)
            { 
                es = new Student(Convert.ToInt32(drows[0][0]));
                es.StudentName = Convert.ToString(drows[0][1]);
            }

            return es;
        }

        public bool UpdateStudent(Student us)
        {
            bool studentUpdated = false;
            DataRow[] drows = DSStudent.Tables["Student"].Select("StudentId=" + us.StudentId);
            if (drows.Length > 0)
            {
                //drows[0][2] = us.Maths;
                //drows[0][3] = us.Physics;
                //drows[0][4] = us.Chemistry;
                studentUpdated = true;
            }

            return studentUpdated;

        }


        public bool DeleteStudent(int sId)
        {
            bool studentDeleted = false;
            DataRow[] drows = DSStudent.Tables["Student"].Select("StudentId=" + sId);
            if (drows.Length > 0)
            {
                DSStudent.Tables[0].Rows.Remove(drows[0]);
                studentDeleted = true;
            }
            return studentDeleted;
        }

        public void SaveDataSetToFile()
        {
            if (File.Exists("Student.xml"))
            {
                File.Delete("Student.xml");
            }

            DSStudent.WriteXml("Student.xml");
        }

        public void LoadDataSetFromFile()
        {
            if(File.Exists("Student.xml"))
            {
            DSStudent.ReadXml("Student.xml");
            }
        }
    }
}
