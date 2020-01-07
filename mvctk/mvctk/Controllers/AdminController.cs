using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvctk.DAL;
using mvctk.Models;
using System.Data.Entity;
using mvctk.ViewModel;
namespace mvctk.Controllers

{
    public class AdminController : Controller
    {
        private SQLContext DB = null;

        public AdminController()
        {
            DB = new SQLContext();
        }

        public ActionResult Admin(user user)
        {
            return View();
        }

        public ActionResult AddCourse()
        {
          //  Session["lecturers"] = DB.users;
            return View();
        }

        [HttpPost]
        public ActionResult Submit(course model)
        {

            if (ModelState.IsValid)

            {

                DB.courses.Add(model);

                DB.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public ActionResult Index()
        {
            return View(DB.courses);
        }

        public ActionResult Delete(String id)
        {
            Session["ID"] = id;
            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find(id);

            if (course == null)
            {

                return HttpNotFound();

            }

            return View(course);

        }



        [HttpPost, ActionName("Delete")]

        public ActionResult DelteConfirmed(string id)

        {

            course course = DB.courses.Find(id);

            DB.courses.Remove(course);

            DB.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find(id);

            if (course == null)

            {

                return HttpNotFound();

            }

            return View(course);

        }

        [HttpPost]

        public ActionResult Edit(course course)

        {
            if (ModelState.IsValid)

            {

                DB.Entry(course).State = EntityState.Modified;

                DB.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(course);

        }
       
    }
}