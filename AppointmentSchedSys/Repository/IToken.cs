using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public interface IToken
    {
        public string CreateToken(UserDTO user);
    }
}
