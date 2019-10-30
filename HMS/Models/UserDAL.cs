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
        public void InsertUser(User obj)
        {
            try {

                db.Users.Add(obj);
                db.SaveChanges();

                
            }
            catch(Exception ex)
            {
              string error=  ex.Message;
            }
            }
        public void UpdateUser(User obj)
        {

            db.Users.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.ClosedByUserID).IsModified = true;
            update.Property(x => x.ClosedOnDate).IsModified = true;
            update.Property(x => x.ClosedReason).IsModified = true;
            update.Property(x => x.Email).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            update.Property(x => x.IsClosed).IsModified = true;
            update.Property(x => x.IsLockedOut).IsModified = true;
            update.Property(x => x.IsSuperUser).IsModified = true;
            update.Property(x => x.UserPassword).IsModified = true;


            update.Property(x => x.UpdatePassword).IsModified = true;
            db.SaveChanges();
        }

        public void InsertUserRoles(UserInRole userRole)
        {
            try
            {

                db.UserInRoles.Add(userRole);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
        public void insertRolePages(tblUserRolePage rolepage)
        {
            try
            {

                db.tblUserRolePages.Add(rolepage);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
        public void deletepages(int roleId)
        {
            var del = db.tblUserRolePages.FirstOrDefault(x => x.RoleId == roleId);
            db.tblUserRolePages.Remove(del);
            db.SaveChanges();
        }


    }
}