using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace barberShop.Models
{
    [Table("tblAppointments")]
    public class Appointment
    {
        [Key]
        public int CustomerID { get; set; }
        [Key]
        public int AppointmentID { get; set; }
        public int Status { get; set; } 
        public DateTime StatusDateTime { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}