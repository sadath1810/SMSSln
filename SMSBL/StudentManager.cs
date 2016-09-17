using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SMSBO;
using System.Data.SqlClient;
using System.Data;

namespace SMSBL
{
    public class StudentManager
    {
        public bool AddStudent(Student s)
        {
            bool studentAdded = false;

            SqlConnection sqlConn = new SqlConnection("Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True");
            //sqlConn.Open();

            string sqlStmt = string.Format("Insert into Student(StudentId, StudentName, Maths, PHysics, Chemistry) values({0},'{1}',{2},{3},{4})", s.StudentId, s.StudentName, s.Maths, s.Physics, s.Chemistry);

            SqlCommand sqlComm = new SqlCommand(sqlStmt, sqlConn);

            sqlComm.Connection.Open();
            int rowsAffected = sqlComm.ExecuteNonQuery();
            if (rowsAffected == 1)
            {
                studentAdded = true;
            }

            sqlComm.Connection.Close();

            return studentAdded;
        }

        public bool UpdateStudent(Student us)
        {
            bool studentUpdated = false;

            SqlConnection sqlConn = new SqlConnection("Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True");
            //sqlConn.Open();

            string sqlStmt = string.Format("Update Student set Maths={0}, PHysics={1}, Chemistry ={2} where studentid={3}", us.Maths, us.Physics, us.Chemistry, us.StudentId);

            SqlCommand sqlComm = new SqlCommand(sqlStmt, sqlConn);

            sqlComm.Connection.Open();
            int rowsAffected = sqlComm.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                studentUpdated = true;
            }

            sqlComm.Connection.Close();

            return studentUpdated;
        }

        public bool DeleteStudent(int sId)
        {
            bool studentDeleted = false;

            SqlConnection sqlConn = new SqlConnection("Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True");
            //sqlConn.Open();

            string sqlStmt = string.Format("Delete from Student  where studentid={0}", sId);

            SqlCommand sqlComm = new SqlCommand(sqlStmt, sqlConn);

            sqlComm.Connection.Open();
            int rowsAffected = sqlComm.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                studentDeleted = true;
            }

            sqlComm.Connection.Close();

            return studentDeleted;
        }

        public Student SelectAStudent(int sId)
        {
            SqlConnection sqlConn = new SqlConnection("Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True");
            //sqlConn.Open();

            string sqlStmt = string.Format("Select StudentId, StudentName, Maths, Physics, Chemistry from Student  where studentid={0}", sId);

            SqlCommand sqlComm = new SqlCommand(sqlStmt, sqlConn);
            SqlDataReader sqlDr = null;
            Student es = null;
            try
            {
                sqlComm.Connection.Open();
                sqlDr = sqlComm.ExecuteReader();

                if (sqlDr.HasRows)
                {
                    while (sqlDr.Read())
                    {
                        es = new Student(Convert.ToInt32(sqlDr[0]));
                        es.StudentName = sqlDr.GetString(1);
                        if (sqlDr[2] != DBNull.Value)
                        {
                            es.Maths = sqlDr.GetInt32(2);
                        }
                        if (sqlDr[3] != DBNull.Value)
                        {
                            es.Physics = sqlDr.GetInt32(3);
                        }
                        if (sqlDr[4] != DBNull.Value)
                        {
                            es.Chemistry = Convert.ToInt32(sqlDr[4]);
                        }
                    }
                }
                
            }
            catch (SqlException sqlEx)
            {
                throw;
            }
            catch (NullReferenceException nex)
            {
                throw nex;
            }
            catch (Exception ex)
            {
                //if you want you log this exception to some log file
                // or log it to some event viewer
                // after you log the exception you have to throw the exception to the calling function
                throw ex;
            }
            finally
            {
                //any resource closures or freeing up resources like file handler must be written in the finally block
                //since finally block will always get executed irrespective of an exception or no exception.
                if (sqlDr != null && !sqlDr.IsClosed)
                { 
                    sqlDr.Close(); 
                }
                if (sqlComm.Connection.State == ConnectionState.Open)
                {
                    sqlComm.Connection.Close();
                }
            }
            return es;
        }

        public List<Student> SelectAllStudents()
        {
            string connString = "Data Source=.;Initial Catalog=SMSBatch1;Integrated Security=True";
            string sqlStmt = string.Format("Select StudentId, StudentName, Maths, Physics, Chemistry from Student; Select CourseId, CourseName from Course");

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlStmt, connString);

            DataSet dsStudent = new DataSet("StudentDB");

            sqlDa.Fill(dsStudent);

            List<Student> ls = new List<Student>();

            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsStudent.Tables[0].Rows)
                {
                    Student es = new Student(Convert.ToInt32(dr[0]));
                    es.StudentName = dr["StudentName"].ToString();
                    if (dr[2] != DBNull.Value)
                    {
                        es.Maths = Convert.ToInt32(dr[2]);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        es.Physics = Convert.ToInt32(dr[3]);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        es.Chemistry = Convert.ToInt32(dr[4]);
                    }

                    ls.Add(es);
                }
            }

            return ls;
        }
    }
}
