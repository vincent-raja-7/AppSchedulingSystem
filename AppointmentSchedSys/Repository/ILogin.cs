using AppointmentSchedSys.DTO;
using AppointmentSchedSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public interface ILogin
    {
        UserDTO Login(UserDTO user);
        UserDTO Register(User user);
    }
}
