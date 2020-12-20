using CleemyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleemyDAL
{
    public static class NatureService
    {
        public static Nature getNatureFromName(string natureName)
        {
            using (CleemyContext db = new CleemyContext())
            {
                return db.Natures.Where(n => n.Name.ToLower() == natureName.ToLower()).FirstOrDefault();
            }
        }
    }
}
