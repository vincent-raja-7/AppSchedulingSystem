using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.DTO
{
    public class UserPassDTO
    {
        public string? Password { get; set; }
        public byte[] Key { get; internal set; }
    }
}
