using dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
 
 
namespace dashboard.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCustomers()
        {
            var pageSize = int.Parse(Request.QueryString["length"]);

            var skip = int.Parse(Request.QueryString["start"]);
            //var searchValue = Request.QueryString["search[value]"];

            var sortColumn = Request.QueryString[string.Concat("columns[", Request.QueryString["order[0][column]"],"][name]")];
            var sortColumnDirection = Request.QueryString["order[0][dir]"];


            IQueryable<Customer> customers = db.Customers;


            //IQueryable<Customer> customers = db.Customers
            //.Where(m => string.IsNullOrEmpty(searchValue) ? true : (m.FirstName.Contains(searchValue)) || (m.LastName.Contains(searchValue)) ||
            //     (m.Contact.Contains(searchValue)) || (m.Email.Contains(searchValue))
            //    );


            if (!string.IsNullOrEmpty(Request.QueryString["columns[0][search][value]"]))
            {
                var firstNameSearchValue = Request.QueryString["columns[0][search][value]"].ToLower();
                customers = customers.Where(x => x.FirstName.ToLower().Contains(firstNameSearchValue));
            }
               
            if (!string.IsNullOrEmpty(Request.QueryString["columns[1][search][value]"]))
            {
                var lastNameSearchValue = Request.QueryString["columns[1][search][value]"].ToLower();
                customers = customers.Where(x => x.LastName.ToLower().Contains(lastNameSearchValue));
            }               
            if (!string.IsNullOrEmpty(Request.QueryString["columns[2][search][value]"]))
            {
                var contactSearchValue = Request.QueryString["columns[2][search][value]"].ToLower();
                customers = customers.Where(x => x.Contact.ToLower().Contains(contactSearchValue));

            }
            if (!string.IsNullOrEmpty(Request.QueryString["columns[3][search][value]"]))
            {
                var emailSearchValue = Request.QueryString["columns[3][search][value]"].ToLower();
                customers = customers.Where(x => x.Email.ToLower().Contains(emailSearchValue));

            }
            if (!string.IsNullOrEmpty(Request.QueryString["columns[4][search][value]"]))
            {
                var DateOfBirthSearchValue = Request.QueryString["columns[4][search][value]"];
                DateTime oDate = Convert.ToDateTime(DateOfBirthSearchValue);
                customers = customers.Where(x => x.DateOfBirth  == oDate);

            }



            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                customers = customers.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
            }

            //switch (sortColumn)
            //{
            //    case "Id":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.Id) : customers.OrderByDescending(a => a.Id);
            //        break;
            //    case "FirstName":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.FirstName) : customers.OrderByDescending(a => a.FirstName);
            //        break;
            //    case "LastName":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.LastName) : customers.OrderByDescending(a => a.LastName);
            //        break;
            //    case "Contact":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.Contact) : customers.OrderByDescending(a => a.Contact);
            //        break;
            //    case "Email":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.Email) : customers.OrderByDescending(a => a.Email);
            //        break;
            //    case "DateOfBirth":
            //        customers = sortColumnDirection == "asc" ? customers.OrderBy(a => a.DateOfBirth) : customers.OrderByDescending(a => a.DateOfBirth);
            //        break;
            //}

            var data = customers.Skip(skip).Take(pageSize).ToList();
            var recordsTotal = customers.Count();


            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal , data  };
          
            return Json( 
             
                jsonData
            , JsonRequestBehavior.AllowGet);

        }
    }
}