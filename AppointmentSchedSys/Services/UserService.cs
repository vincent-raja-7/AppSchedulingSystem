using AppointmentSchedSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public class UserService : IRepo<string, User>
    {
        private readonly AppSysContext _context;

        public UserService(AppSysContext context)
        {
            _context = context;
        }

        public User Add(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public User Delete(string key)
        {
            throw new NotImplementedException();
        }

        public User Get(string key)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == key);
            return user;
        }

        public ICollection<User> GetAll()
        {
            if (_context.Users.Count() > 0)
                return _context.Users.ToList();
            return null;
        }

        public User Update(User u)
        {
            var user = Get(u.Username);
            if (user != null)
            {
                try
                {
                    HMACSHA512 hmac = new HMACSHA512();
                    user.Key = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(u.Password)); 
                    user.Name = u.Name;
                    user.Email = u.Email;
                    _context.SaveChanges();
                    return user;
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
