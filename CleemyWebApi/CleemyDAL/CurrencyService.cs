using CleemyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleemyDAL
{
    public class CurrencyService
    {
        /// <summary>
        /// Get nature entity from code
        /// </summary>
        /// <param name="natureCode">null if not found, entity otherwise</param>
        /// <returns></returns>
        public static Currency getNatureFromName(string natureCode)
        {
            using (CleemyContext db = new CleemyContext())
            {
                return db.Currencies.Where(c => c.Code.ToLower() == natureCode.ToLower()).FirstOrDefault();
            }
        }

    }
}
