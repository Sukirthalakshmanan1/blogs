using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmpOperation
    {
        public List<BLClass1> GetAllEmployeeDetails()
        {
            MyContext1 context = new MyContext1();

            List<EmpInfo> clist = context.EmpTable.ToList();
            List<BLClass1> cblist = new List<BLClass1>();
            foreach (var item in clist)
            {
                cblist.Add(new BLClass1 { EmailId=item.EmailId,Name=item.Name,DateOfJoining=item.DateOfJoining,PassCode=item.PassCode });



            }
            return cblist;




        }
        public bool Insert(BLClass1 bal)
        {
            try
            {
                MyContext1 context = new MyContext1();

                EmpInfo b = new EmpInfo();
                b.EmailId = bal.EmailId;
                b.Name = bal.Name;
                b.DateOfJoining = bal.DateOfJoining;
                b.PassCode = bal.PassCode;
                
                context.EmpTable.Add(b);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DeleteEmployeeDetails(int id)
        {
            try
            {
                MyContext1 context = new MyContext1();

                List<EmpInfo> s = context.EmpTable.ToList();
                EmpInfo r = s.Find(pr => pr.PassCode == id);

                context.EmpTable.Remove(r);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public BLClass1 search(int id)
        {
            MyContext1 context = new MyContext1();
            List<EmpInfo> customers = context.EmpTable.ToList();
            EmpInfo obj = customers.Find(cust => cust.PassCode== id);

            // List<BLClass1> cblist = new List<BLClass1>();
            BLClass1 b = new BLClass1();
            b.EmailId = obj.EmailId;
            b.Name = obj.Name;
            b.DateOfJoining = obj.DateOfJoining;
            b.PassCode = obj.PassCode;
            


            return b;

            //context.SaveChanges();
        }
        public bool UpdateEmployeeDetails(BLClass1 bal)
        {
            try
            {
                MyContext1 context = new MyContext1();
                List<EmpInfo> customers = context.EmpTable.ToList();
                EmpInfo obj = customers.Find(cust => cust.PassCode == bal.PassCode);
                obj.Name = bal.Name;
                obj.DateOfJoining = bal.DateOfJoining;
                obj.PassCode = bal.PassCode;
               
                

                // context.Updatebookdetails();
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
