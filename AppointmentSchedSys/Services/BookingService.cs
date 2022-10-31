using AppointmentSchedSys.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public class BookingService : IRepo<DateTime, Bookings>
    {
        private readonly AppSysContext _context;

        public BookingService(AppSysContext context)
        {
            _context = context;
        }

        public Bookings Add(Bookings item)
        {
            try
            {
                _context.Bookings.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public Bookings Delete(DateTime key)
        {
            var b = Get(key);
            if (b != null)
            {
                try
                {
                    _context.Bookings.Remove(b);
                    _context.SaveChanges();
                    return b;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            return null;
        }
        public Bookings Get(DateTime key)
        {
            var booking = _context.Bookings.Include(book => book.User_1).Include(u2 => u2.User_2).Include(u3 => u3.User_3).FirstOrDefault(b => b.Date == key);
            return booking;
        }

        public ICollection<Bookings> GetAll()
        {
            var bookings = _context.Bookings.ToList();
            return bookings;
        }

        public Bookings Update(Bookings item)
        {
            var b = Get(item.Date);
            if (b != null)
            {
                try
                {
                    b.Slot_1 = item.Slot_1;
                    b.Slot_2 = item.Slot_2;
                    b.Slot_3 = item.Slot_3;
                    _context.SaveChanges();
                    return b;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            return null;
        }
    }
}
