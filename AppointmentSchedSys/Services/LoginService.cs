using AppointmentSchedSys.DTO;
using AppointmentSchedSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Services
{
    public class LoginService : ILogin
    {
        private readonly IRepo<string, User> _repo;
        private readonly IToken _tokenService;

        public LoginService(IRepo<string, User> repo, IToken tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }
        public UserDTO Login(UserDTO user)
        {
            var myUser = _repo.GetAll().FirstOrDefault(u => u.Username == user.Username);
            if (myUser != null)
            {
                var dbPass = myUser.PasswordHash;
                HMACSHA512 hmac = new HMACSHA512(myUser.Key);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < dbPass.Length; i++)
                {
                    if (userPass[i] != dbPass[i])
                        return null;
                }
                user.Password = null;
                user.Token = _tokenService.CreateToken(user);
                return user;
            }
            return null;
        }

        public UserDTO Register(User user)
        {
            HMACSHA512 hmac = new HMACSHA512();
            user.Key = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            var myUser = _repo.Add(user);
            if (myUser != null)
                return new UserDTO
                {
                    Username = user.Username,
                    Token = _tokenService.CreateToken(new UserDTO { Username = user.Username })
                };
            return null;
        }
    }
}
