using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using barberShop.Models;

namespace barberShop.Controllers
{

    public class AppointmentController : Controller
    {
 
        // GET: Appointment
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home");

            AppointmentContext appointmentContext = new AppointmentContext();
            List<Customer> customers = appointmentContext.Customer.ToList();
            List<Appointment> appointments = appointmentContext.appointments.ToList();


            FullDetails fullDetails;
            List<FullDetails> fullDetailsList = new List<FullDetails>();

            int id;

            foreach (Customer customer in customers)
            {
                id = customer.CustomerID;
                foreach(Appointment appointment in appointments.Where(c => c.CustomerID == id) )
                {
                    fullDetails = new FullDetails();
                    fullDetails.customer = customer;
                    fullDetails.appointment = appointment;

                    fullDetailsList.Add(fullDetails);
                }
            }
            return View(fullDetailsList);
        }


        [HttpGet]
        public ActionResult Edit(int customerID, int appointmentID)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home");
            
            AppointmentContext appointmentContext = new AppointmentContext();
            Appointment appointment = appointmentContext.appointments.Single(_appointment => _appointment.CustomerID == customerID && _appointment.AppointmentID == appointmentID);

            return PartialView("Create", appointment);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_post(Appointment appointment)
        {
            if (!errorApointment(appointment.AppointmentDateTime))
                return RedirectToAction("Index", "Appointment");

            if (ModelState.IsValid)
            {
                AppointmentContext appointmentContext = new AppointmentContext();
                appointmentContext.UpdateAppointment(appointment);
                return RedirectToAction("Index");             
            }    
            return View(appointment);
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home");
            //return View();
            return PartialView("Create");
        }

        
        [HttpPost]
        public ActionResult Create(DateTime appointmentDateTime)
        {
            //The function checks if it possible to detrmine appointment at the required time
            if (!errorApointment(appointmentDateTime))
                return RedirectToAction("Index", "Appointment");

            int id = Convert.ToInt32(Session["CustomerId"]);  

            Appointment appointment = new Appointment();
            TryUpdateModel<Appointment>(appointment);       

            appointment.CustomerID = id;
            appointment.AppointmentID = 0;
            appointment.Status = 1;
            appointment.StatusDateTime = DateTime.Now;
            appointment.AppointmentDateTime = appointmentDateTime;

            AppointmentContext appointmentContext = new AppointmentContext();

            appointmentContext.AddAppointment(appointment);

            return RedirectToAction("Index", "Appointment");
        }

        public ActionResult Delete(Appointment appointment)
        {
            AppointmentContext appointmentContext = new AppointmentContext();
            appointmentContext.CancelAppointment(appointment);
            return RedirectToAction("Index");
        }

        //Pop-up
        public ActionResult Details(int customerID, int appointmentID)
        {
            AppointmentContext appointmentContext = new AppointmentContext();
            FullDetails fullDetails = new FullDetails();
            fullDetails.customer = appointmentContext.Customer.Single(_customer => _customer.CustomerID == customerID);
            fullDetails.appointment = appointmentContext.appointments.Single(_appointment => _appointment.CustomerID == customerID && _appointment.AppointmentID == appointmentID );
            return PartialView("Details", fullDetails);
        }


        //The function checks if it possible to detrmine appointment at the required time
        private Boolean errorApointment(DateTime appointmentDateTime)
        {

            //Case the user enter time that over
            if (appointmentDateTime < DateTime.Now)
            {
                TempData["timeOver"] = "מועד התור המבוקש חלף";
                return false;
            }

            //Case the barbershop is close 
            string day = appointmentDateTime.DayOfWeek.ToString();
            if (day == "Saturday")
            {
                TempData["barbershopClose"] = "המספרה סגורה במועד המבוקש";
                return false;
            }
            else if (day == "Friday")
            {
                if (appointmentDateTime.Hour < 9 || appointmentDateTime.Hour > 14)
                {
                    TempData["barbershopClose"] = "המספרה סגורה במועד המבוקש";
                    return false;
                }
            }
            else
            {
                if (appointmentDateTime.Hour < 9 || appointmentDateTime.Hour > 18)
                {
                    TempData["barbershopClose"] = "המספרה סגורה במועד המבוקש" ;
                    return false;
                }
            }

            //Case the time of the appointment does not equal to xx:00 or xx:30
            if (appointmentDateTime.Minute != 30 && appointmentDateTime.Minute != 00)
            {
                TempData["errorTime"] = "לא ניתן לקבוע תור בשעה לא עגולה או חצי עגולה";
                return false;
            }
            AppointmentContext appointmentContext = new AppointmentContext();

            //Case the appointment was caughted
            foreach (Appointment _appointment in appointmentContext.appointments.Where(a => a.AppointmentDateTime < appointmentDateTime.AddHours(1) && a.AppointmentDateTime > appointmentDateTime.AddHours(-1) && a.Status != 9))
            {
                TempData["notFreeApointnet"] = "מועד התור תפוס";
                return false;
            }          
           
            return true;
        }
    }
}