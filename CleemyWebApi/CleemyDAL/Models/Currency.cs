using System;
using System.Collections.Generic;

#nullable disable

namespace CleemyDAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Expenses = new HashSet<Expense>();
            LuccaUsers = new HashSet<LuccaUser>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<LuccaUser> LuccaUsers { get; set; }
    }
}
