using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarLogging.Models
{
    public class LogViewModel
    {
        [Key]
        public int? LogID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Car_Model { get; set; }
        public decimal? Miles_Driven { get; set; }
        public decimal? Gallons_Filled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}