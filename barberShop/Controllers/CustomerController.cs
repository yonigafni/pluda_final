using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using barberShop.Models;

namespace barberShop.Controllers
{
    public class CustomerController : Controller
    {


        [HttpGet]
        [ActionName("Register")]
        public ActionResult Registe_get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult Register_post()
        {

            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                TryUpdateModel<Customer>(customer);

                Session["CustomerId"] = customer.CustomerID;
                Session["UserName"] = customer.UserName.ToString();
                Session["FirstName"] = customer.FirstName.ToString();
                Session["LastName"] = customer.LastName.ToString();

                AppointmentContext appointmentContext = new AppointmentContext();
                try
                {
                    appointmentContext.AddCustomer(customer);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "הלקוח רשום במערכת";
                    return View();
                }      
                return RedirectToAction("Index", "Appointment");
            }
            return View();
        }

        
    }
}