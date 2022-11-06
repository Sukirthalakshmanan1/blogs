using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class EmpUIController : Controller
    {
        // GET: EmpUI
        MyContext1 db = new MyContext1();
        EmpOperation e = new EmpOperation();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(AdminModel log)
        {
            var user = db.AdminTable.Where(x => x.EmailId == log.EmailId && x.pass == log.Pass).Count();
            if (user > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            List<EmpModel> emplist = new List<EmpModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var responseTask = client.GetAsync("Emps");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<EmpModel[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new EmpModel
                        {
                            EmailId = item.EmailId,
                            Name = item.Name,
                            DateOfJoining = item.DateOfJoining,
                            PassCode = item.PassCode
                        });

                    }
                }
            }
            return View(emplist);

        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Create(EmpModel empmodel)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/Emps");

                var emp = new EmpModel
                {
                    EmailId = empmodel.EmailId,
                    Name = empmodel.Name,
                    DateOfJoining = empmodel.DateOfJoining,
                    PassCode = empmodel.PassCode
                };

                var postTask = client.PostAsJsonAsync<EmpModel>(client.BaseAddress, emp);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<EmpModel>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                }


            }

            return RedirectToAction("Index");
        }

        public ActionResult EditEmp(int id)
        {
            var emp = e.search(id);
            EmpModel model = new EmpModel();
            model.PassCode = id;
            model.EmailId = emp.EmailId;
            model.Name = emp.Name;
            model.DateOfJoining = emp.DateOfJoining;
            return View(model);
        }

        // POST: Emp/Edit/5
        [HttpPost]
        public ActionResult EditEmp(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var emp = e.search(id);
                emp.PassCode = Convert.ToInt32(Request["PassCode"]);
                emp.EmailId = Request["EmailId"].ToString();
                emp.Name = Request["Name"].ToString();
                emp.DateOfJoining = Convert.ToDateTime(Request["DateOfJoining"]);
                bool ans = e.UpdateEmployeeDetails(emp);
                if (ans)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            var emp = e.search(id);
            EmpModel model = new EmpModel();
            model.EmailId = emp.EmailId;
            model.Name = emp.Name;
            model.DateOfJoining = emp.DateOfJoining;
            model.PassCode = emp.PassCode;


            return View(model);
        }

        // POST: Emp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var dataFound = e.search(id);
                if (dataFound != null)
                {
                    bool ans = e.DeleteEmployeeDetails(id);
                    if (ans)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var data = e.search(id);
            EmpModel model = new EmpModel();
            model.EmailId = data.EmailId;
            model.Name = data.Name;
            model.DateOfJoining = data.DateOfJoining;
            model.PassCode = data.PassCode;

            return View(model);
        }

    }
}