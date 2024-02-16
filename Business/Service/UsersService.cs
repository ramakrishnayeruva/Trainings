using DataAccess.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using DataAccess.Data;

namespace Business.Service
{
    public class UsersService : IUser
    {
        private TrainingsApiDBContext dbContext;
       
        public UsersService(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;
          
        }
        public List<User> GetUsers()
        {
            try
            {
                return dbContext.Users.ToList();
            }
            catch
            {
                throw;
            }
        }
        public User GetUsers(int id)
        {
            try
            {
                User? user = dbContext.Users.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
       
        public void AddUsers(User addUserRequest)
        {            
            try
            {
                dbContext.Users.Add(addUserRequest);
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public User DeleteEmployee(int id)
        {
            try
            {
                User? user = dbContext.Users.Find(id);

                if (user != null)
                {
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        public User DeleteUser(int id)
        {
            try
            {
                User? user = dbContext.Users.Find(id);

                if (user != null)
                {
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        public bool CheckUser(int id)
        {
            return dbContext.Users.Any(e => e.Id == id);
        }
    }
}
