using AppointmentSchedSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Repository
{
    public interface IFindRepo
    {
        ICollection<AppointmentEntries> FindByUsername(string user);
        ICollection<AppointmentEntries> FindByStatus(string status);
        ICollection<AppointmentEntries> FindByUserAndStatus(string user, string status);
    }
}
