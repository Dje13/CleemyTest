using CleemyDAL.Models;
using CleemyWebApi.Controllers;
using CleemyWebApi.DTO;
using CleemyWebApi.Validator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleemyUnitTests
{
    public class CreateExpenseUnitTest
    {

        protected LuccaUser _testUser;
        protected Nature _testNature;
        protected Currency _testCurrencyNotUser;

        public CreateExpenseUnitTest()
        {
            using (CleemyContext db = new CleemyContext())
            {
                _testUser = db.LuccaUsers.Include("Expenses").Include("Currency").Include("Currency").Where(u => u.Expenses.Count > 0).FirstOrDefault();
                _testNature = db.Natures.FirstOrDefault();
                long curUserCurrency = _testUser.Currency.Id;
                _testCurrencyNotUser = db.Currencies.Where(c => c.Id != curUserCurrency).FirstOrDefault();
            }
        }

        public ExpenseDTO createExpenseDTO()
        {
            return new ExpenseDTO
            {
                luccaUserId = _testUser.Id,               
                nature = _testNature.Name,
                currency = _testUser.Currency.Code
            };
        }

        [Fact]
        public void testDateFuture()
        {
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.dateExpense = DateTime.Now.AddDays(1);

            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            Assert.True(messages.Where(m => m == ExpenseValidator.messageDateFuture).Any());
        }


        [Fact]
        public void testDateOlderThan3Month()
        {
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.dateExpense = DateTime.Now.AddDays(-95);
            

            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            Assert.True(messages.Where(m => m == ExpenseValidator.messageDate3Months).Any());
        }


        [Fact]
        public void testCommentEmpty()
        {
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.commentExpense = "";

            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            Assert.True(messages.Where(m => m == ExpenseValidator.messageComment).Any());
        }

        [Fact]
        public void testUnicity()
        {
            LuccaUser curUser = _testUser;
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.dateExpense = curUser.Expenses.FirstOrDefault().DateExpense;
            myDTO.amount = curUser.Expenses.FirstOrDefault().Amount;

            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            Assert.True(messages.Where(m => m == ExpenseValidator.messageUnicityExpense).Any());
        }


        [Fact]
        public void testCurrencyMatch()
        {
            LuccaUser curUser = _testUser;
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.dateExpense = DateTime.Now;
            myDTO.amount = curUser.Expenses.FirstOrDefault().Amount-10;
            myDTO.commentExpense = " Should work ";
            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            
            Assert.True(messages.Count == 0 );
        }

        [Fact]
        public void testExpenseOK()
        {
            LuccaUser curUser = _testUser;
            ExpenseDTO myDTO = createExpenseDTO();
            myDTO.dateExpense = DateTime.Now;
            myDTO.amount = curUser.Expenses.FirstOrDefault().Amount - 10;
            myDTO.currency = _testCurrencyNotUser.Code;
            List<string> messages = ExpenseValidator.validateExpense(myDTO);
            Assert.True(messages.Where(m => m == ExpenseValidator.messageCurrencyMatch).Any());
        }

    }

}
