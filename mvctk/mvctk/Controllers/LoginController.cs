using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvctk.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult signin()
        {
            return View(new user());
        }
        [HttpPost]
        public ActionResult Submit(user user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Student", "Student");
            }
            else
            {
                return View("signin", user);
            }
        }
        
      
    }
}