using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class UserDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public List<User> GetAllUser()
        {
            return db.Users.OrderByDescending(x => x.ID).ToList();
        }

        public User GetUserByID(int UserID)
        {
            return db.Users.Where(x => x.ID == UserID).FirstOrDefault();
        }
    }
}