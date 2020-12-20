using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleemyDAL;
using CleemyDAL.Models;
using CleemyWebApi.DTO;


namespace CleemyWebApi.Mapping
{
    public static class ExpenseMapper
    {

        /// <summary>
        /// Map an expense entity from DTO
        /// </summary>
        /// <param name="expenseDTO"></param>
        /// <param name="expense"></param>
        public static void mapExpenseFromDTO(ExpenseDTO expenseDTO, Expense expense)
        {
            Mapper.mapObject(expenseDTO, expense);

            LuccaUser curUser = UserService.getUserFromId(expenseDTO.luccaUserId);
            if (curUser  == null)
            {
                throw new Exception("User not found");
            }
            Nature curNature =  NatureService.getNatureFromName(expenseDTO.nature);
            if (curNature == null)
            {
                throw new Exception("Nature not found");
            }
            expense.NatureId = curNature.Id;

            Currency curCurrency =  CurrencyService.getNatureFromName(expenseDTO.currency);
            if (curCurrency == null)
            {
                throw new Exception("Nature not found");
            }
            expense.CurrencyId = curCurrency.Id;

        }


        /// <summary>
        /// Map base expense DTO from entity
        /// </summary>
        /// <param name="expense">entity with dependancies</param>
        /// <param name="expenseDTO"></param>
        public static void mapDTOFromExpense(Expense expense, BaseExpenseDTO expenseDTO)
        {
            Mapper.mapObject(expense, expenseDTO,WithId:true);
            expenseDTO.currency = expense.Currency.Code;
            expenseDTO.nature = expense.Nature.Name;
        }


        /// <summary>
        /// Map Expense DTO for list from entity
        /// </summary>
        /// <param name="expense">entity with dependancies</param>
        /// <param name="expenseDTO"></param>
        public static void mapDTOForListFromExpense(Expense expense, ExpenseForListDTO expenseDTO)
        {
            mapDTOFromExpense(expense, expenseDTO);
            expenseDTO.luccaUSer = expense.LuccaUser.FirstName + " " + expense.LuccaUser.LastName;
            expenseDTO.currencyName = expense.Currency.Name;

        }

    }
}
