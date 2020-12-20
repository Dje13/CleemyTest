using CleemyDAL;
using CleemyDAL.Models;
using CleemyWebApi.DTO;
using CleemyWebApi.Mapping;
using CleemyWebApi.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace CleemyWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : Controller
    {
        [HttpPost]
        public ActionResult createExpense(ExpenseDTO expenseDTO)
        {
            //DataFactory.AddTicket(ticket);

            List<string> listError = ExpenseValidator.validateExpense(expenseDTO);
            if (listError.Count >0)
            {
                return Problem(JsonSerializer.Serialize(listError));
            }
            Expense curExpense = new Expense();
            ExpenseMapper.mapExpenseFromDTO(expenseDTO, curExpense);
            curExpense = ExpenseService.addExpense(curExpense);
            curExpense = ExpenseService.getExpenseFromId(curExpense.Id);
            ExpenseDTO result = new ExpenseDTO();
            ExpenseMapper.mapDTOFromExpense(curExpense,result);

            return Ok(result);
        }

        
        [HttpGet]
        [ResponseType(typeof(List<ExpenseForListDTO>))]
        public ActionResult getExpenses([FromQuery] long userId  , [FromQuery] string sortField = "")
        {
            List<ExpenseForListDTO> result = new List<ExpenseForListDTO>();
            List<Expense> expenses = ExpenseService.getExpensesForOneUser(userId, sortField);

            foreach (Expense expense in expenses)
            {
                ExpenseForListDTO expenseForListDTO = new ExpenseForListDTO();
                ExpenseMapper.mapDTOForListFromExpense(expense, expenseForListDTO);
                result.Add(expenseForListDTO);
            }
            return Ok(result);
        }

        

    }
}
