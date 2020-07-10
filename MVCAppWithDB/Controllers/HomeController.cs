using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperations;

namespace MVCAppWithDB.Controllers
{
    public class HomeController : Controller
    {

        EmployeesRepository repository = null;
        public HomeController()
        {
            repository = new EmployeesRepository();
        }


        // GET: Home
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddEmployee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Data Added";
                }
            }
            return View();
        }

        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();
            return View(result);
        }


        public ActionResult Details(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }


        public ActionResult Edit(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEmployee(model.id, model);
                return RedirectToAction("GetAllRecords");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {          
                repository.DeleteEmployee(id);
                return RedirectToAction("GetAllRecords");
        }
    }
}