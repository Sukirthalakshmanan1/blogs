using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    
    public class Blogmodel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        public string subject { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string BlogUrl { get; set; }
        public string EmpEmailId { get; set; }
    }
}