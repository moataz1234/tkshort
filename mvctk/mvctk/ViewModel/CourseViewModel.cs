using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvctk.ViewModel
{
    public class CourseViewModel
    {
        public  course course { get; set; }
        public List<course> courses { get; set; }
    }
}