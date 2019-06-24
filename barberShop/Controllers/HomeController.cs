using barberShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace barberShop.Controllers
{

    public class HomeController : Controller
    {
        // GET: home
        
        [HttpGet]
        [ActionName("Login")]
        public ActionResult Login(string UserName, string Password)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public ActionResult Login_post(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                using (AppointmentContext appointmentContext = new AppointmentContext())
                {
                    List<Customer> customers = appointmentContext.Customer.ToList();
                    var obj = customers.Where(a => a.UserName.Equals(userName) && a.Password.Equals(password.ComputeSha256Hash())).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["CustomerID"] = obj.CustomerID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["FirstName"] = obj.FirstName.ToString();
                        Session["LastName"] = obj.LastName.ToString(); 
                        return RedirectToAction("Index", "Appointment");
                    } 
                    else if(userName == "" || password == "")
                        return View();
                    else
                    {
                        TempData["invalidCustomer"] = "שם משתמש או סיסמה שגויים";
                        return View();
                    }
                }               
            }
            TempData["invalidConnection"] = "פרטי התחברות שגויים";
            return View();
        }

    }
}