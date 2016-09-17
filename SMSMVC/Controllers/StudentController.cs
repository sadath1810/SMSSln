
using SMSBL;
using SMSBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSMVC.Controllers
{
    public class StudentController : Controller
    {
        
        
        
        //
        // GET: /Student/

        public ActionResult Index()
        {
            StudentManager sm = new StudentManager();
            List<Student> ls = sm.SelectAllStudents();


            //ViewBag.Message = TempData["Message"];
            ViewData["Message"] = TempData["Message"];
            return View(ls);
        }

        public ActionResult Details(int id)
        {
            StudentManager sm = new StudentManager();
            Student s = sm.SelectAStudent(id);
            
            return View(s);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            StudentManager sm = new StudentManager();
            bool studentAdded = sm.AddStudent(s);
            if (studentAdded)
            {
                TempData["Message"] = "Student Added successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.Message = "Student addition failed!!!";
                ViewData["Message"] = "Student addition failed!!!";
                return View();
            }
            
        }

        public ActionResult Edit(int id)
        {
            StudentManager sm = new StudentManager();
            Student s =  sm.SelectAStudent(id);
            return View(s);
        }

        [HttpPost]
        public ActionResult Edit(Student s)
        {
            StudentManager sm = new StudentManager();
            bool studentUpdated = sm.UpdateStudent(s);

            if (studentUpdated)
            {
                TempData["Message"] = "Student Updated successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.Message = "Student updation failed!!!";
                ViewData["Message"] = "Student updation failed!!!";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            StudentManager sm = new StudentManager();
            Student s = sm.SelectAStudent(id);
            return View(s);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int StudentId)
        {
            StudentManager sm = new StudentManager();
            bool studentDeleted = sm.DeleteStudent(StudentId);

            if (studentDeleted)
            {
                TempData["Message"] = "Student Deleted successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.Message = "Student updation failed!!!";
                ViewData["Message"] = "Student deletion failed!!!";
                return View();
            }
        }
    }
}
