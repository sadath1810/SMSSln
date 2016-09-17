﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMSConsole
{
    class Program
    {
        static Dictionary<int,Student> sArr = new Dictionary<int, Student>();
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
            //We will read from the file only if it exists. There can be scenarios like the first run of the program where the file is not even created
            if (File.Exists(@"Student.txt"))
            {
                TextReader tr = new StreamReader(@"Student.txt");

                string stuRec = null;
                while ((stuRec = tr.ReadLine()) != null)
                {
                    //stuRec will have something like "12|sadath|56|78|56"
                    string[] stuDetails = stuRec.Split('|');

                    Student s = new Student(Convert.ToInt32(stuDetails[0]));
                    s.StudentName = stuDetails[1];
                    s.Maths = Convert.ToInt32(stuDetails[2]);
                    s.Physics = Convert.ToInt32(stuDetails[3]);
                    s.Chemistry = Convert.ToInt32(stuDetails[4]);

                    sArr.Add(s.StudentId, s);
                }

                tr.Close();
            }
        }

        private static void WriteInfoToFile()
        {
            //It makes sense to delete the old file if it exists, since we have the latest information in the dictionary
            if (File.Exists(@"Student.txt"))
            {
                //File.Copy(@"Student.txt", @"D:\Work\Student" + DateTime.Now.ToFileTime() + ".txt");
                
                File.Delete(@"Student.txt");
            }


            TextWriter tw = new StreamWriter(@"Student.txt");

            foreach (Student es in sArr.Values)
            {
                tw.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}",es.StudentId,es.StudentName,es.Maths,es.Physics,es.Chemistry));
            }
            tw.Close();
        }

        private static void ShowAllStudents()
        {
            foreach(Student es in sArr.Values)
            {
                if (es != null)
                {
                    Console.WriteLine(es.ToString());
                    //Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                }
            }
        }

        private static void ShowAStudent()
        {
            Console.WriteLine("You have selected to show a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());
            
            if(sArr.ContainsKey(sId))
            {
                Student es = sArr[sId];
                if (es != null)
                {
                    Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                }
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("You have selected to delete a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());

            bool studentDeleted = false;

            if(sArr.ContainsKey(sId))
            {
                sArr.Remove(sId);
                studentDeleted = true;
            }

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

            bool studentUpdated = false;

            if (sArr.ContainsKey(us.StudentId))
            {
                Student es = sArr[us.StudentId];
                if (es != null)
                {
                    es.Maths = us.Maths;
                    es.Physics = us.Physics;
                    es.Chemistry = us.Chemistry;
                    studentUpdated = true;
                }
            }
            
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

            bool studentAdded = false;

            if (!sArr.ContainsKey(s.StudentId))
            {
                sArr.Add(s.StudentId, s);
                studentAdded = true;
            }

            if (studentAdded)
            {
                Console.WriteLine("Student added successfully");
            }
            else
            {
                Console.WriteLine("Student addition failed");
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
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
    }
}
