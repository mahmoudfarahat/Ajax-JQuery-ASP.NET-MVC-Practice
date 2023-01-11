using dashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    public class EmployeesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Employee
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Get(int start = 0, int length = 10)
        {
            var search = Request.QueryString.Get("search[value]");
            var orderColumn = Request.QueryString.Get("order[0][column]");
            var orderDir = Request.QueryString.Get("order[0][dir]");

            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            var employees = db.Employees.Where(a => a.Name.Contains(search));

            switch (orderColumn)
            {
                case "1":
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.Id) : employees.OrderByDescending(a => a.Id);
                    break;
                case "2":
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.Name) : employees.OrderByDescending(a => a.Name);
                    break;
                case "3":
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.Email) : employees.OrderByDescending(a => a.Email);
                    break;
                case "4":
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.Phone) : employees.OrderByDescending(a => a.Phone);
                    break;
                case "5":
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.HireDate) : employees.OrderByDescending(a => a.HireDate);
                    break;
                default:
                    employees = orderDir == "asc" ? employees.OrderBy(a => a.Id) : employees.OrderByDescending(a => a.Id);
                    break;
            }


            employees = employees.Skip(start).Take(length);
               

            return Json( new {
                recordsFiltered = db.Employees.Where(a => a.Name.Contains(search)).Count(),
                recordsTotal = db.Employees.Count(),
                data= employees.ToList()}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmployees(List<int> ids)
        {
            foreach (var id in ids)
            {
              var employee = db.Employees.Find(id);
               db.Employees.Remove(employee);
               db.SaveChanges();
            }
            return Json(ids);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int? id)
        {
            if(id == 0)
            {
                return View(new Employee());
            }
            return View(db.Employees.Find(id));
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee employee)
        {
            if(employee.Id == 0)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}