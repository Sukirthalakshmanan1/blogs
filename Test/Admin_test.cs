using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Admin_test
    {
        MyContext1 db = new MyContext1();
        public bool check()
        {
            bool ans = false;
            var found = db.AdminTable.ToList();
            if (found[0].EmailId == "suki123@gmail.com" && found[0].pass == "suki123")
            {
                ans = true;
            }
            return ans;
        }
    }
}