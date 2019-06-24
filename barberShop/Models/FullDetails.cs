using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using barberShop.Models;
using System.Data.Entity;

namespace barberShop.Models
{

    public class FullDetails
    {
        public Customer customer { get; set; }
        public Appointment appointment { get; set; }
    }
}