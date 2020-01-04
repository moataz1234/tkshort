using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvctk.Models;

namespace mvctk.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin(user user)
        {
            return View();
        }
    }
}