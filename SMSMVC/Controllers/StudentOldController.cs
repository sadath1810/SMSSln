using SMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSMVC.Controllers
{
    public class StudentOldController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index()
        {
            List<Student> ls = new List<Student>()
                                {
                                    new Student{StudentId = 10, StudentName="Sadath", Maths=100, Physics=90, Chemistry=89},
                                    new Student{StudentId = 12, StudentName="Vasu", Maths=80, Physics=70,Chemistry=65}
                                };

            return View(ls);
        }

        public ActionResult Details(int id)
        {
            //
            Student s = new Student() { StudentId = id, StudentName = "Gopal", Maths = 90, Physics = 80, Chemistry = 77 };

            return View(s);
        }

    }
}
