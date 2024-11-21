using Buisenss.Interface.Abstract;
using DataAccess;
using Entity.Concrate;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Buisenss.Interface.Service
{
    
    public class UserService : IUserService, IAllService<User>
    {
        private readonly ArticleContext _db;
        public UserService(ArticleContext db)
        {
            _db = db;
        }

        public UserService()
        {
        }

        public async Task Add(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
        public async Task<User> GetUserByName(string UserName)
        {
             return await _db.Users.Where(x=> x.UserName == UserName).FirstOrDefaultAsync();
        }
        public async Task<User> CheckUserName(string UserName)
        {
            return await _db.Users.Where(x=> x.UserName == UserName).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetList()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetById(int? userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task Update(User user)
        {
            var existUser = await _db.Users.FindAsync(user.UserId);
            if (existUser != null)
            {
                existUser.Password = user.Password;
                _db.Entry(existUser).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }

        }

        public async Task Delete(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
        public User ValidateUser(string userName, string Password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName && x.Password == Password);
            return user;
        }
        public async Task<User> UserLog(User user)
        {
          
            var userLog = await _db.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            return userLog;
        }
        public bool CheckAuthorization(User user)
        {
            if (user == null)
            {
                return false;
            }
            return user.IsActive;
        }

        public async Task<IPagedList<User>> GetFilterListOrAllList(string Search, int Page)
        {
            var userList = await _db.Users.ToListAsync();
            var query = from user in userList select user;
            if (!String.IsNullOrEmpty(Search))
            {
                query = userList.Where(x=>x.UserName.Contains(Search));
            }
           return query.OrderByDescending(x => x.UserId).ToPagedList(Page, 10);
        }
        private string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var item in bytes)
                {
                    builder.Append(item.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
