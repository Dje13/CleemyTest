using CleemyDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleemyDAL
{
    public static class ExpenseService
    {
        /// <summary>
        /// add the entity in database
        /// </summary>
        /// <param name="curExpense"></param>
        /// <returns>entity persisted (with id affected)</returns>
        public static Expense addExpense(Expense curExpense)
        {
            using (CleemyContext db = new CleemyContext())
            {
                db.Expenses.Add(curExpense);
                db.SaveChanges();
                return curExpense;
            }
        }

        /// <summary>
        /// return an expense searched by id 
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns>entity including dependancies</returns>
        public static Expense getExpenseFromId(long expenseId)
        {
            Expense result;
            using (CleemyContext db = new CleemyContext())
            {
                result = db.Expenses.Include("Nature").Include("Currency").Include("LuccaUser").Where(e=> e.Id == expenseId).FirstOrDefault() ;
            }
            return result;
        }

        /// <summary>
        /// Get expenses list for a user, sort by sortfield if not empty
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sortField"></param>
        /// <returns>List of entities including dependancies</returns>
        public static List<Expense> getExpensesForOneUser(long userId,string sortField)
        {
            List<Expense> result;
            using (CleemyContext db = new CleemyContext())
            {
                result = db.Expenses.Where(e=>e.LuccaUserId == userId).Include("Nature").Include("Currency").Include("LuccaUser").ToList();
                if (!String.IsNullOrEmpty(sortField))
                {
                    switch (sortField.ToLower()) {

                        case "amount":
                            result = result.OrderBy(e => e.Amount).ToList();
                            break;
                        case "dateexpense":
                            result = result.OrderBy(e => e.DateExpense).ToList();
                            break;
                    }

                }
            }
            return result;
        }
    }
    
}
