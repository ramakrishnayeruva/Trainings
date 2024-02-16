using DataAccess.model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        void AddUsers(User addUserRequest);
        User GetUsers(int id);
        void UpdateUser(User user);
        bool CheckUser(int id);
        User DeleteUser(int id);
       // Task<IActionResult> GetUser([FromRoute] int id);
       // Task<IActionResult> UpdateUser([FromRoute] int id, User updateUserRequest);
       // Task<IActionResult> DeleteUser(int id);

    }
}


