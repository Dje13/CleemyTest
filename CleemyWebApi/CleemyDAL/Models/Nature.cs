using System;
using System.Collections.Generic;

#nullable disable

namespace CleemyDAL.Models
{
    public partial class Nature
    {
        public Nature()
        {
            Expenses = new HashSet<Expense>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
