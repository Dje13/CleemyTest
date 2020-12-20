using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleemyWebApi.DTO
{
    public class BaseExpenseDTO
    {
        public long id { get; set; }
        public DateTime dateExpense { get; set; }
        public string nature { get; set; }
        
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string commentExpense { get; set; }

    }
}
