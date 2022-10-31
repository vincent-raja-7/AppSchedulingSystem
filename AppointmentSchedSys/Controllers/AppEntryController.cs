using AppointmentSchedSys.Models;
using AppointmentSchedSys.Repository;
using AppointmentSchedSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class AppEntryController : ControllerBase
    {
        private readonly IRepo<int, AppointmentEntries> _repo;
        private readonly IFindRepo _frepo;

        public AppEntryController(IRepo<int, AppointmentEntries> repo, IFindRepo frepo)
        {
            _repo = repo;
            _frepo = frepo;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult<AppointmentEntries> AddAppointment(AppointmentEntries book)
        {
            _repo.Add(book);
            return Ok("Entry Added"); 
        }

        [Authorize]
        [HttpGet]
        [Route("FindById")]
        public ActionResult<AppointmentEntries> GET(int id)
        {
            return Ok(_repo.Get(id));
        }

        [Authorize]
        [HttpGet]
        [Route("FindAll")]
        public ActionResult<AppointmentEntries> GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(AppointmentEntries e)
        {
            _repo.Update(e);
            return Ok("Entry Updated");
        }

        [Authorize]
        [HttpGet]
        [Route("FindByUserAndStatus")]
        public ActionResult<AppointmentEntries> GetByUserAndStatus(String status, String user)
        {
            return Ok(_frepo.FindByUserAndStatus(user,status));
        }

        [Authorize]
        [HttpGet]
        [Route("FindByUser")]
        public ActionResult<AppointmentEntries> GetByUser(String user)
        {
            return Ok(_frepo.FindByUsername(user));
        }

        [Authorize]
        [HttpGet]
        [Route("FindByStatus")]
        public ActionResult<AppointmentEntries> GetByStatus(String status)
        {
            return Ok(_frepo.FindByStatus(status));
        }
    }
}
