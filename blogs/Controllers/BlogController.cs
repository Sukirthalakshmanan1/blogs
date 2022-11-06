using BLL;
using blogs.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace blogs.Controllers
{
    public class BlogController : ApiController
    {
        // GET api/<controller>
        BlogOperation obj = null;
        public BlogController()
        {
            obj = new BlogOperation();
        }

        // [Route("GetAllMarks")]
        [HttpGet]
        public List<bl> GetBlogList()
        {
            //bl --model
            //BLclass1
            List<BLClass2> empbal = new List<BLClass2>();
            empbal = obj.GetAllDetails();
            List<bl> emps = new List<bl>();
            foreach (var item in empbal)
            {
                //Employees emp = new Employees();
                emps.Add(new bl { BlogId = item.BlogId, Title = item.Title, subject = item.subject, BlogUrl = item.BlogUrl, DateOfCreation = item.DateOfCreation ,EmpEmailId=item.EmpEmailId});
            }
            return emps;

        }
        public HttpResponseMessage PostDetails([FromBody] bl empdata)
        {
            BLClass2 empbal = new BLClass2();
            empbal.BlogId = empdata.BlogId;
            empbal.Title = empdata.Title;
            empbal.subject = empdata.subject;
            empbal.BlogUrl = empdata.BlogUrl;
            empbal.DateOfCreation=  empdata.DateOfCreation;
            empbal.EmpEmailId = empdata.EmpEmailId;



            bool ans = obj.Insert(empbal);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }
        [Route("FindById/{id:int?}")]
        public bl GetBlogByID(int id)
        {
            BLClass2 empbal = new BLClass2();
            empbal = obj.search(id);
            bl emp = new bl();
            //emp.Id = empbal.Id;
            emp.BlogId= id;
            emp.Title = empbal.Title;
            emp.subject = empbal.subject;
            emp.BlogUrl = empbal.BlogUrl;
            emp.DateOfCreation= empbal.DateOfCreation;
            emp.EmpEmailId=empbal.EmpEmailId;

            return emp;

        }
        public HttpResponseMessage PutDetails([FromBody] bl empdata)
        {

            BLClass2 empbal = new BLClass2();
            empbal.BlogId = empdata.BlogId;
            empbal.Title = empdata.Title;
            empbal.subject = empdata.subject;
            empbal.BlogUrl = empdata.BlogUrl;
            empbal.DateOfCreation=empdata.DateOfCreation;
            empbal.EmpEmailId=empdata.EmpEmailId;

            bool ans = obj.UpdateDetails(empbal);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteBlogDetails(int id)
        {
            bool ans = obj.DeleteDetails(id);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }
    }
}