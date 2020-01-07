using mvctk.Models;
using System.Linq;
using System.Web.Mvc;

namespace mvctk.Controllers
{
    public class LoginController : Controller
    {
        private SQLContext DB = null;

        public LoginController()
        {

            DB = new SQLContext();
        }

        public ActionResult signin()
        {
            return View(new user());
        }
        [HttpPost]
        public ActionResult Submit(user user)
        {
            if (ModelState.IsValid)
            {
                var Logged = DB.users.FirstOrDefault(x => x.ID.Equals(user.ID));
                if (Logged != null)
                {
                    if (Logged.PassWord.Equals(user.PassWord))
                    {


                        if (Logged.UserTyper == 0)
                            return RedirectToAction("Student", "Student", Logged);

                        if (Logged.UserTyper == 1)
                            return RedirectToAction("Lecturer", "Lecturer", Logged);

                        if (Logged.UserTyper == 2)
                            return RedirectToAction("Admin", "Admin", Logged);
                    }
                    else
                        ModelState.AddModelError("FirstName", "The username or password is incorrect");
                }
                else
                    ModelState.AddModelError("FirstName", "The username or password is incorrect");
            }
            return View("signin", user);

        }


    }
}