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
        private SQLContext DB = null;

        public LoginController() {

            DB = new SQLContext();
        }

        public ActionResult signin()
        {
            return View(new user());
        }
        [HttpPost]
        public ActionResult Submit(user user)
        {
            var Logged = DB.users.FirstOrDefault(x => x.ID.Equals(user.ID));
            if (Logged != null)
            {
                if (Logged.PassWord.Equals(user.PassWord))
                {

                    if (ModelState.IsValid)
                    {
                        if (Logged.UserTyper == 0)
                            return RedirectToAction("Student", "Student");
                    }
                }
            }
            return View("signin", user);
            
        }
        
      
    }
}