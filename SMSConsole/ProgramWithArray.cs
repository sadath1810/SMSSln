using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSConsole
{
    class Program
    {
        static Student[] sArr = null;
        static void Main(string[] args)
        {
            Console.WriteLine("How many students?");
            int sCount = Convert.ToInt32(Console.ReadLine());
            sArr = new Student[sCount];
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
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] != null)
                {
                    Student es = sArr[i];
                    Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                }
            }
        }

        private static void ShowAStudent()
        {
            Console.WriteLine("You have selected to show a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] != null)
                {
                    Student es = sArr[i];
                    if (es.StudentId == sId)
                    {
                        Console.WriteLine(string.Format("StudentId:{0} StudentName: {1} Maths: {2} Physics: {3} Chemistry:{4}", es.StudentId, es.StudentName, es.Maths, es.Physics, es.Chemistry));
                        break;
                    }
                }
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("You have selected to delete a student");
            Console.WriteLine("Enter StudentId:");
            int sId = Convert.ToInt32(Console.ReadLine());

            bool studentDeleted = false;
            int delIndex = -1;
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] != null)
                {
                    Student es = sArr[i];
                    if (es.StudentId == sId)
                    {
                        delIndex = i;
                        break;
                    }
                }
            }

            if (delIndex > -1)
            {
                sArr[delIndex] = null;
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

            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] != null)
                {
                    Student es = sArr[i];

                    if (es.StudentId == us.StudentId)
                    {
                        es.Maths = us.Maths;
                        es.Physics = us.Physics;
                        es.Chemistry = us.Chemistry;
                        studentUpdated = true;
                        break;
                    }
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

            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] == null)
                {
                    sArr[i] = s;
                    studentAdded = true;
                    break;
                }
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
