using System;
using System.Collections.Generic;

#nullable disable

namespace CleemyDAL.Models
{
    public partial class LuccaUser
    {
        public LuccaUser()
        {
            Expenses = new HashSet<Expense>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
