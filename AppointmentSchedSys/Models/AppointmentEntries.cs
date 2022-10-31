using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Models
{
    public class AppointmentEntries
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }
        public DateTime? Rescheduled_Date { get; set; }
        public int? Rescheduled_Slot { get; set; }
        public String Status { get; set; }
        public String Note { get; set; }
        public String Notification { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
