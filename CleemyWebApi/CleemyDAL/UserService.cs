using CleemyDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleemyDAL
{
    public static class UserService
    {
        public static LuccaUser getUserFromId(long id)
        {
            using (CleemyContext db = new CleemyContext())
            {
                return db.LuccaUsers.Find(id);
            }
        }
    }
}
