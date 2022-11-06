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
    public class bloguiController : Controller
    {
        // GET: blogui
        MyContext1 db = new MyContext1();
        BlogOperation ob = new BlogOperation();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(EmpModel log)
        {
            var user = db.EmpTable.Where(x => x.EmailId == log.EmailId && x.PassCode == log.PassCode).Count();
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
            List<Blogmodel> emplist = new List<Blogmodel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var responseTask = client.GetAsync("Blog");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Blogmodel[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new Blogmodel
                        {
                            BlogId = item.BlogId,
                            Title = item.Title,
                            subject = item.subject,
                            DateOfCreation = item.DateOfCreation,
                            BlogUrl = item.BlogUrl,
                            EmpEmailId = item.EmpEmailId
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

        public ActionResult Create(Blogmodel empmodel)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320//api/Blog");

                var emp = new Blogmodel
                {
                    BlogId = empmodel.BlogId,
                    Title = empmodel.Title,
                    subject = empmodel.subject,
                    DateOfCreation = empmodel.DateOfCreation,
                    BlogUrl = empmodel.BlogUrl,
                    EmpEmailId = empmodel.EmpEmailId
                };

                var postTask = client.PostAsJsonAsync<Blogmodel>(client.BaseAddress, emp);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<Blogmodel>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                }


            }

            return RedirectToAction("Index");
        }

        public ActionResult EditBlog(int id)
        {
            var emp = ob.search(id);
            Blogmodel model = new Blogmodel();
            model.BlogId = id;
            model.Title = emp.Title;
            model.subject = emp.subject;
            model.DateOfCreation = emp.DateOfCreation;
            model.BlogUrl = emp.BlogUrl;
            model.EmpEmailId = emp.EmpEmailId;
            return View(model);
        }

        // POST: Emp/Edit/5
        [HttpPost]
        public ActionResult EditBlog(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var emp = ob.search(id);
                emp.BlogId = Convert.ToInt32(Request["BlogId"]);
                emp.Title = Request["Title"].ToString();
                emp.subject = Request["subject"].ToString();
                emp.DateOfCreation = Convert.ToDateTime(Request["DateOfCreation"]);
                emp.BlogUrl = Request["BlogUrl"].ToString();
                emp.EmpEmailId = Request["EmpEmailId"].ToString();
                bool ans = ob.UpdateDetails(emp);
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

        public ActionResult DeleteBlog(int id)
        {
            var emp = ob.search(id);
            Blogmodel model = new Blogmodel();
            model.BlogId = id;
            model.Title = emp.Title;
            model.subject = emp.subject;
            model.DateOfCreation = emp.DateOfCreation;
            model.BlogUrl = emp.BlogUrl;
            model.EmpEmailId = emp.EmpEmailId;
            return View(model);
        }

        // POST: Emp/Delete/5
        [HttpPost]
        public ActionResult DeleteBlog(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var dataFound = ob.search(id);
                if (dataFound != null)
                {
                    bool ans = ob.DeleteDetails(id);
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


        public ActionResult BlogDetails(int id)
        {
            var data = ob.search(id);
            Blogmodel emp = new Blogmodel();
            emp.BlogId = id;
            emp.Title = data.Title;
            emp.subject = data.subject;
            emp.DateOfCreation = data.DateOfCreation;
            emp.BlogUrl = data.BlogUrl;
            emp.EmpEmailId = data.EmpEmailId;
            return View(emp);
        }


    }
}