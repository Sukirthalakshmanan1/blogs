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
    public class EmpsController : ApiController
    {
        // GET api/<controller>
        EmpOperation obj = null;
        public EmpsController()
        {
            obj = new EmpOperation();
        }

        // [Route("GetAllMarks")]
        [HttpGet]
        public List<Emp> GetEmployeeList()
        {
            //sub_mark --model
            //BLclass1
            List<BLClass1> empbal = new List<BLClass1>();
            empbal = obj.GetAllEmployeeDetails();
            List<Emp> emps = new List<Emp>();
            foreach (var item in empbal)
            {
                //Employees emp = new Employees();
                emps.Add(new Emp { EmailId = item.EmailId, Name = item.Name, DateOfJoining = item.DateOfJoining, PassCode = item.PassCode });
            }
            return emps;

        }
        public HttpResponseMessage PostEmpDetails([FromBody] Emp empdata)
        {
            BLClass1 empbal = new BLClass1();
            empbal.EmailId = empdata.EmailId;
            empbal.Name = empdata.Name;
            empbal.DateOfJoining = empdata.DateOfJoining;
            empbal.PassCode = empdata.PassCode;
            


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
        public Emp GetEmployeeByID(int id)
        {
            BLClass1 empbal = new BLClass1();
            empbal = obj.search(id);
            Emp emp = new Emp();
            //emp.Id = empbal.Id;
            emp.EmailId = empbal.EmailId;
            emp.Name = empbal.Name;
            emp.DateOfJoining= empbal.DateOfJoining;
            emp.PassCode= id;
            
            return emp;

        }
        public HttpResponseMessage PutEmpDetails([FromBody] Emp empdata)
        {

            BLClass1 empbal = new BLClass1();
            empbal.EmailId = empdata.EmailId;
            empbal.Name = empdata.Name;
            empbal.DateOfJoining = empdata.DateOfJoining;
            empbal.PassCode = empdata.PassCode;
            

            bool ans = obj.UpdateEmployeeDetails(empbal);
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
        public HttpResponseMessage DeleteEmpDetails(int id)
        {
            bool ans = obj.DeleteEmployeeDetails(id);
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