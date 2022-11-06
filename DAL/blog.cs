using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BlogOperation
    {
        public List<BLClass2> GetAllDetails()
        {
            MyContext1 context = new MyContext1();

            List<BlogInfo> clist = context.BlogTable.ToList();
            List<BLClass2> cblist = new List<BLClass2>();
            foreach (var item in clist)
            {
                cblist.Add(new BLClass2 { BlogId=item.BlogId,Title=item.Title,subject=item.subject,BlogUrl=item.BlogUrl,DateOfCreation=item.DateOfCreation,EmpEmailId=item.EmpEmailId });



            }
            return cblist;




        }
        public bool Insert(BLClass2 bal)
        {
            try
            {
                MyContext1 context = new MyContext1();

                BlogInfo b = new BlogInfo();
                b.BlogId = bal.BlogId;
                b.Title = bal.Title;
                b.subject = bal.subject;
                b.BlogUrl = bal.BlogUrl;
                b.DateOfCreation = bal.DateOfCreation;
                b.EmpEmailId = bal.EmpEmailId;

                context.BlogTable.Add(b);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DeleteDetails(int id)
        {
            try
            {
                MyContext1 context = new MyContext1();

                List<BlogInfo> s = context.BlogTable.ToList();
                BlogInfo r = s.Find(pr => pr.BlogId == id);

                context.BlogTable.Remove(r);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public BLClass2 search(int id)
        {
            MyContext1 context = new MyContext1();
            List<BlogInfo> customers = context.BlogTable.ToList();
            BlogInfo obj = customers.Find(cust => cust.BlogId == id);

            // List<BLClass1> cblist = new List<BLClass1>();
            BLClass2 b = new BLClass2();
            b.BlogId = obj.BlogId;
            b.Title = obj.Title;
            b.subject = obj.subject;
            b.BlogUrl = obj.BlogUrl;
            b.DateOfCreation=obj.DateOfCreation;
            b.EmpEmailId=obj.EmpEmailId;


            return b;

            //context.SaveChanges();
        }
        public bool UpdateDetails(BLClass2 bal)
        {
            try
            {
                MyContext1 context = new MyContext1();
                List<BlogInfo> customers = context.BlogTable.ToList();
                BlogInfo obj = customers.Find(cust => cust.BlogId == bal.BlogId);
                obj.Title = bal.Title;
                obj.subject = bal.subject;
                obj.BlogUrl = bal.BlogUrl;
                obj.DateOfCreation=bal.DateOfCreation;

                obj.EmpEmailId = bal.EmpEmailId;

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
