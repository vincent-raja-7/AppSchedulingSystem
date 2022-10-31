using AppointmentSchedSys.Models;
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
    public class BookingController : ControllerBase
    {
        private readonly IRepo<DateTime, Bookings> _repo;

        public BookingController(IRepo<DateTime, Bookings> repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult<Bookings> AddBooking(Bookings book)
        {
            var b = _repo.Add(book);
            return Ok("Booking Added");
        }
        [Authorize]
        [HttpGet]
        [Route("GetByDate")]
        public ActionResult<Bookings> GET(DateTime date)
        {
            return Ok(_repo.Get(date));
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult <Bookings> Update(Bookings b)
        {
            _repo.Update(b);
            return Ok("Booking Updated");
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete (DateTime date)
        {
            _repo.Delete(date);
            return Ok("Booking Deleted");
        }

        [Authorize]
        [HttpGet]
        [Route("FindAll")]
        public ActionResult <Bookings> GetAll()
        {
            return Ok(_repo.GetAll());
        }
    }
}
