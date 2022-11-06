using DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class blogapp_test
    {
        [TestCase]
        public void Employee_mail_Test()
        {

            MyContext1 db = new MyContext1();
            var found = db.AdminTable.ToList();

            Assert.AreEqual("suki123@gmail.com", found[0].EmailId);
            Console.WriteLine("Validation success");

        }
      

        [TestCase]
        public void Admin_password_Test()
        {

            MyContext1 db = new MyContext1();
            var found = db.AdminTable.ToList();

            Assert.AreEqual("suki123", found[0].pass);
            Console.WriteLine("Validation success");

        }
        Admin_test t = new Admin_test();
        [TestCase]
        public void AdminTest()
        {
            t.check();


        }
    }
}