using CleemyDAL.Models;
using CleemyWebApi.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleemyWebApi.Validator
{
    public static class ExpenseValidator
    {

        public static string messageDateFuture = "Une dépense ne peut pas avoir une date dans le futur";
        public static string messageDate3Months = "Une dépense ne peut pas être datée de plus de 3 mois";

        public static string messageComment = "Le commentaire est obligatoire";

        public static string messageCurrencyMatch = "La devise de la dépense doit être identique à celle de l'utilisateur";

        public static string messageUnicityExpense = "Un utilisateur ne peut pas déclarer deux fois la même dépense (même date et même montant)";




        /// <summary>
        /// validate an expense
        /// </summary>
        /// <param name="myExpense">Expense to validate</param>
        /// <returns>List of errors if validation fails, empty list otherwise</returns>
        public static List<string> validateExpense(ExpenseDTO myExpense)
        {
            List<string> errorMessages = new List<string>();

            checkDate(myExpense, errorMessages);
            checkComment(myExpense, errorMessages);

            checkFromDb(myExpense, errorMessages);
            return errorMessages;
        }


        /// <summary>
        /// Validate Date value 
        /// </summary>
        /// <param name="myExpense">Expense to validate</param>
        /// <param name="errorMessages">list of error, add new error(s) in teh list check fails</param>
        private static void checkDate(ExpenseDTO myExpense, List<string> errorMessages)
        {
            // Check date not in future 
            if (myExpense.dateExpense.Date.CompareTo(DateTime.Now.Date) > 0  )
            {
                errorMessages.Add(messageDateFuture);
            }

            // check date > 3 month
            if (myExpense.dateExpense.Date.CompareTo(DateTime.Now.AddMonths(-3).Date) < 0)
            {
                errorMessages.Add(messageDate3Months);
            }

        }

        /// <summary>
        /// Validate Comment value 
        /// </summary>
        /// <param name="myExpense">Expense to validate</param>
        /// <param name="errorMessages">list of error, add new error(s) in teh list check fails</param>
        private static void checkComment(ExpenseDTO myExpense, List<string> errorMessages)
        {
            if (String.IsNullOrEmpty(myExpense.commentExpense))
            {
                errorMessages.Add(messageComment);
            }
        }


        /// <summary>
        /// Validate Expense from existing data in db
        /// </summary>
        /// <param name="myExpense">Expense to validate</param>
        /// <param name="errorMessages">list of error, add new error(s) in teh list check fails </param>
        private static void checkFromDb(ExpenseDTO myExpense, List<string> errorMessages)
        {

            using (CleemyContext db = new CleemyContext())
            {
                string[] possibleNature = db.Natures.Select(s => s.Name).ToArray();

                if (String.IsNullOrEmpty(myExpense.nature) || !possibleNature.Where(n=>n.ToLower() == myExpense.nature.ToLower()).Any())
                {
                    errorMessages.Add(String.Format("Nature {0} Inconnue, valeur possible {1} ", myExpense.nature, String.Join(',',possibleNature)));
                }
                LuccaUser curUser = db.LuccaUsers.Include("Expenses").Include("Currency").Include("Currency").Where(u => u.Id == myExpense.luccaUserId).FirstOrDefault() ;
                if (curUser == null)
                {
                    errorMessages.Add(String.Format("Utilisateur {0} Inconnu ", myExpense.luccaUserId));
                }
                else
                {
                    if (curUser.Currency.Code != myExpense.currency)
                    {
                        errorMessages.Add(messageCurrencyMatch);
                    }
                    if (curUser.Expenses.Where(e=>e.Amount == myExpense.amount && e.DateExpense.Date.CompareTo(myExpense.dateExpense.Date) ==0 ).Any())
                    {
                        errorMessages.Add(messageUnicityExpense);
                    }

                }
            }
        }
    }
}
