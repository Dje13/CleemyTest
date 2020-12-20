using System;
using System.Collections.Generic;

#nullable disable

namespace CleemyDAL.Models
{
    public partial class Expense
    {
        public long Id { get; set; }
        public DateTime DateExpense { get; set; }
        public long NatureId { get; set; }
        public long LuccaUserId { get; set; }
        public long CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string CommentExpense { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual LuccaUser LuccaUser { get; set; }
        public virtual Nature Nature { get; set; }
    }
}
