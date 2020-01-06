using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mvctk.Controllers
{
    public class gradeController : Controller
    {
        private SQLContext DB = null;

        public gradeController()
        {
            DB = new SQLContext();
        }

        public ActionResult show()
        {
            return View(DB.grades);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grade grade = DB.grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }
        [HttpPost]
        public ActionResult Edit(grade grade)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(grade).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("show");
            }
            return View(grade);
        }
    }
}