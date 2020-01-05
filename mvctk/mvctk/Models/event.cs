using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvctk.Models
{
    public class @event
    {
        private string name;
        private string time;
        private string classroom;

        public @event(string name, string time, string classroom)
        {
            this.name = name;
            this.time = time;
            this.classroom = classroom;
        }
    }
}