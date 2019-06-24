using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace barberShop.Models
{
    public class AppointmentContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public IEnumerable<Appointment> appointments
        {
            get
            {
                List<Appointment> appointments = new List<Appointment>();
                Appointment appointment;
                string query = "select * from tblAppointments";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppointmentContext"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        appointment = new Appointment();
                        appointment.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                        appointment.AppointmentID = Convert.ToInt32(rdr["AppointmentID"]);
                        appointment.Status = Convert.ToInt32(rdr["Status"]);
                        appointment.StatusDateTime = Convert.ToDateTime(rdr["StatusDateTime"]);
                        appointment.AppointmentDateTime = Convert.ToDateTime(rdr["AppointmentDateTime"]);

                        appointments.Add(appointment);
                    }
                    return appointments;
                }  
            }
        }

        public void AddCustomer(Customer customer)
        {
            string query = "insert into tblCustomers values(@cid, @fname, @lname, @usr, @pwd)";
 
            this.Database.ExecuteSqlCommand(query,
            new SqlParameter("@cid", customer.CustomerID),
            new SqlParameter("@fname", customer.FirstName),
            new SqlParameter("@lname", customer.LastName),
            new SqlParameter("@usr", customer.UserName),
            new SqlParameter("@pwd", customer.Password.ComputeSha256Hash())
            );
 
            
        }

        public void AddAppointment(Appointment appointment)
        {

            string query = "insert into tblAppointments values(@cid, @appid, @stt, @sttdt, @appdt)";

            // Get next appointmentId for specified customer from DB 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppointmentContext"].ConnectionString))
            {
                con.Open();
                SqlCommand cmdGetId = new SqlCommand("select dbo.getNextAppointmentID(" + appointment.CustomerID.ToString() + ")", con);
                appointment.AppointmentID = (int)cmdGetId.ExecuteScalar();
                con.Close();
            }

            this.Database.ExecuteSqlCommand(query, 
                new SqlParameter("@cid", appointment.CustomerID),
                new SqlParameter("@appid", appointment.AppointmentID),
                new SqlParameter("@stt", appointment.Status),
                new SqlParameter("@sttdt", appointment.StatusDateTime),
                new SqlParameter("@appdt", appointment.AppointmentDateTime)
                );
        }

        public void UpdateAppointment(Appointment appointment)
        {
            int stt = 2; // or any other logic

            string query =
                "update tblAppointments " +
                "set Status = @stt, StatusDateTime = @sttdt, AppointmentDateTime = @appdt " +
                "where CustomerID = @cid and AppointmentID = @appid";

            this.Database.ExecuteSqlCommand(query,
                new SqlParameter("@cid", appointment.CustomerID),
                new SqlParameter("@appid", appointment.AppointmentID),
                new SqlParameter("@stt", stt),
                new SqlParameter("@sttdt", DateTime.Now),
                new SqlParameter("@appdt", appointment.AppointmentDateTime)
                );
        }

        public void CancelAppointment(Appointment appointment)
        {
            int stt = 9; // or any other logic

            string query = 
                "update tblAppointments set Status = @stt " +
                "where CustomerID = @cid and AppointmentID = @appid";

            this.Database.ExecuteSqlCommand(query,
                new SqlParameter("@cid", appointment.CustomerID),
                new SqlParameter("@appid", appointment.AppointmentID),
                new SqlParameter("@stt", stt)
                );
        }

    }
}