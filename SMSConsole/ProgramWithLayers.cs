using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using SMSBO;
using SMSBL;

namespace SMSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadInfoFromFile();
            int choice = 0;
            while ((choice = DisplayMenuAndGetChoice()) != 6)
            {
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        UpdateStudent();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:
                        ShowAStudent();
                        break;
                    case 5:
                        ShowAllStudents();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Please enter again.");
                        break;
                }
            }
            Console.WriteLine("Writing/persisting data into file");
            WriteInfoToFile();
            Console.WriteLine("Finished writing/persisting data into file");
            Console.WriteLine("Exiting SMS!!! Press any key to exit.");
            Console.ReadLine();
        }

        private static void ReadInfoFromFile()
        {
            //since we are directly talking to a database, there is no need to read from any file
        }

        private static void WriteInfoToFile()
        {
            //since we are directly talking to a database, there is no need to write to any file
        }

        //ShowAllStudents with SqlDataReader - ADO.Net Connected Architecture
        //private static void ShowAllStudents()
        //{
        //    SqlConnection sqlConn = new SqlConnection("Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True");
        //    //sqlConn.Open();

        //    string sqlStmt = string.Format("Select StudentId, StudentName, Maths, Physics, Chemistry from Student; Select CourseId, CourseName from Course");

        //    SqlCommand sqlComm = new SqlCommand(sqlStmt, sqlConn);

        //    sqlComm.Connection.Open();
        //    SqlDataReader sqlDr = sqlComm.ExecuteReader();


        //    List<Student> ls = new List<Student>();

        //    if (sqlDr.HasRows)
        //    {
        //        while (sqlDr.Read())
        //        {
        //            Student es = new Student(Convert.ToInt32(sqlDr[0]));
        //            es.StudentName = sqlDr.GetString(1);
        //            if (sqlDr[2] != DBNull.Value)
        //            {
        //                es.Maths = sqlDr.GetInt32(2);
        //            }
        //            if (sqlDr[3] != DBNull.Value)
        //            {
        //                es.Physics = sqlDr.GetInt32(3);
        //            }
        //            if (sqlDr[4] != DBNull.Value)
        //            {
        //                es.Chemistry = Convert.ToInt32(sqlDr[4]);
        //            }

        //            ls.Add(es);
        //        }

        //        if (sqlDr.NextResult())
        //        {
        //            if (sqlDr.HasRows)
        //            {
        //                while (sqlDr.Read())
        //                {
        //                    //assume you are creating a course object here
        //                    //Course c = new Course(Convert.ToInt32(sqlDr[0]));
        //                    //c.CourseName = sqlDr[1].GetString(1);
        //                }
        //            }
        //        }
        //    }
        //    sqlDr.Close();
        //    sqlComm.Connection.Close();

        //    foreach(Student es1 in ls)
        //    {
        //        if (es1 != null)
        //        {
        //            Console.WriteLine(es1.ToString());
        //            //Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
        //        }
        //    }
        //}

        //ShowAllStudents with disconnected architecture - SqlDataAdapter 
        private static void ShowAllStudents()
        {

            StudentManager sm = new StudentManager();
            List<Student> ls = sm.SelectAllStudents();

            foreach (Student es1 in ls)
            {
                if (es1 != null)
                {
                    Console.WriteLine(es1.ToString());
                    //Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                }
            }
        }

        private static void ShowAStudent()
        {
            try
            {
                Console.WriteLine("You have selected to show a student");
                Console.WriteLine("Enter StudentId:");
                int sId = Convert.ToInt32(Console.ReadLine());

                StudentManager sm = new StudentManager();
                Student es = sm.SelectAStudent(sId);

                if (es != null)
                {
                    Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("sql error occured." + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while retrieving student. Please try again" + ex.Message);
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("You have selected to delete a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());

            StudentManager sm = new StudentManager();
            bool studentDeleted = sm.DeleteStudent(sId);

            if (studentDeleted)
            {
                Console.WriteLine("Student deleted successfully");
            }
            else
            {
                Console.WriteLine("Student deletion failed");
            }

        }

        private static void UpdateStudent()
        {
            Console.WriteLine("You have selected to update a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter New Maths:");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter New Physics:");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter New Chemistry:");
            int c = Convert.ToInt32(Console.ReadLine());

            Student us = new Student(sId);
            us.Maths = m;
            us.Physics = p;
            us.Chemistry = c;

            StudentManager sm = new StudentManager();
            bool studentUpdated = sm.UpdateStudent(us);

            if (studentUpdated)
            {
                Console.WriteLine("Student updated successfully");
            }
            else
            {
                Console.WriteLine("Student updation failed");
            }
        }

        private static void AddStudent()
        {
            try
            {
                Console.WriteLine("You have selected to add a student");
                Console.WriteLine("Enter StudentId:");
                int sId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter StudentName:");
                string sName = Console.ReadLine();
                Console.WriteLine("Enter Maths:");
                int m = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Physics:");
                int p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Chemistry:");
                int c = Convert.ToInt32(Console.ReadLine());

                Student s = new Student(sId);
                s.StudentName = sName;
                s.Maths = m;
                s.Physics = p;
                s.Chemistry = c;
            

                StudentManager sm = new StudentManager();
                bool studentAdded =  sm.AddStudent(s);

                if (studentAdded)
                {
                    Console.WriteLine("Student added successfully");
                }
                else
                {
                    Console.WriteLine("Student addition failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("You have entered invalid student details. Please check and correct." + ex.Message);
            }
        }

        private static int DisplayMenuAndGetChoice()
        {
            Console.WriteLine("SMS Menu!!!");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student");
            Console.WriteLine("3. Delete Student");
            Console.WriteLine("4. Show A Student");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter your choice:");
            
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6." + ex.Message);
            }
            return choice;
        }
    }
}
