using AppointmentSchedSys.Models;
using AppointmentSchedSys.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public class AppEntryService : IRepo<int, AppointmentEntries>, IFindRepo
    {
        private readonly AppSysContext _context;

        public AppEntryService(AppSysContext context)
        {
            _context = context;
        }

        public AppointmentEntries Add(AppointmentEntries item)
        {
            try
            {
                _context.AppointmentEntries.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public AppointmentEntries Delete(int key)
        {
            throw new NotImplementedException();
        }

        public AppointmentEntries Get(int key)
        {
            var entry = _context.AppointmentEntries.Include(e=>e.User).FirstOrDefault(e => e.Id == key);
            return entry;
        }

        public ICollection<AppointmentEntries> GetAll()
        {
            var entires = _context.AppointmentEntries.Include(e => e.User).ToList();
            return entires;
        }

        public AppointmentEntries Update(AppointmentEntries item)
        {
            var e = Get(item.Id);
            if (e != null)
            {
                try
                {
                    e.Notification = item.Notification;
                    e.Note = item.Note;
                    e.Status = item.Status;
                    e.Rescheduled_Date = item.Rescheduled_Date;
                    e.Rescheduled_Slot = item.Rescheduled_Slot;
                    _context.SaveChanges();
                    return e;
                }
                catch (Exception s)
                {

                    throw;
                }
            }
            return null;
        }

        public ICollection<AppointmentEntries> FindByStatus(String status)
        {
            var entries = _context.AppointmentEntries.Include(e => e.User).Where(e => e.Status == status).ToList();
            return entries;
        }

        public ICollection<AppointmentEntries> FindByUserAndStatus(string user, String status)
        {
            var entries = _context.AppointmentEntries.Where(e => e.User.Username == user && e.Status == status).ToList();
            return entries;
        }

        public ICollection<AppointmentEntries> FindByUsername(string user)
        {
            var entries = _context.AppointmentEntries.Where(e => e.User.Username == user).ToList();
            return entries;
        }
    }
}
