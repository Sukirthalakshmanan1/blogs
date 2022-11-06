using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLClass1
    {
        public string EmailId { get; set; }
        
        public string Name { get; set; }



        public DateTime DateOfJoining { get; set; }
        public int PassCode { get; set; }

    }
    public class BLClass2
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        public string subject { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string BlogUrl { get; set; }
        public string EmpEmailId { get; set; }
    }
}
