using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAjax.Data;
using PeopleAjax.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAjax.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
        @"Data Source=.\sqlexpress; Initial Catalog=People;Integrated Security=true;";       
        public IActionResult Index()
        {            
            return View();
        }
        public IActionResult GetPeople()
        {
            var db = new PeopleDB(_connectionString);
            var ppl = db.GetPeople();
            return Json(ppl);
        }
        [HttpPost]
        public IActionResult AddPerson(Person p)
        {
            var db = new PeopleDB(_connectionString);
            db.AddPerson(p);
            return Json(p);
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            var db = new PeopleDB(_connectionString);
            db.DeletePerson(id);
            return Json(id);
        }
        [HttpPost]
        public IActionResult EditPerson(Person p)
        {
            var db = new PeopleDB(_connectionString);
            db.EditPerson(p);
            return Json(p);

        }
    }
}
