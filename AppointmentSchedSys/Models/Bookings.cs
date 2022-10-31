using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User_1")]
        public int? Slot_1 { get; set; }
        [ForeignKey("User_2")]
        public int? Slot_2 { get; set; }
        [ForeignKey("User_3")]
        public int? Slot_3 { get; set; }

        
        public User User_1 { get; set; }
       
        public User User_2 { get; set; }
        
        public  User User_3 { get; set; }
    }
}
