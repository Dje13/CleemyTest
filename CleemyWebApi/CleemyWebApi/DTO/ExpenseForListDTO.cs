using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleemyWebApi.DTO
{
    public class ExpenseForListDTO:BaseExpenseDTO
    {

        public string luccaUSer { get; set; }
        public string currencyName { get; set; }
    }
}
