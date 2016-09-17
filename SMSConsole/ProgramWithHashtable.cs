using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SMSConsole
{
    class Program
    {
        static Hashtable sArr = new Hashtable();
        static void Main(string[] args)
        {
            
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

            Console.WriteLine("Exiting SMS!!! Press any key to exit.");
            Console.ReadLine();
        }

        private static void ShowAllStudents()
        {
            foreach(object o in sArr.Values)
            {
                if (o != null)
                {
                    Student es = (Student)o;
                    Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
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
                object o = sArr[sId];
                if (o != null)
                {
                    Student es = (Student)o;
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
                object o = sArr[us.StudentId];
                if (o != null)
                {
                    Student es = (Student)o;
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
                //sArr.Add("Hello", "You fool!!");
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
