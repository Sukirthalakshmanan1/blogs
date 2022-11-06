using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {

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


    }
}