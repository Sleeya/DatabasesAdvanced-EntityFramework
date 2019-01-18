using BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Initializer.Initializers
{
    internal class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            BankAccount[] bankAccounts = new BankAccount[]
            {
                new BankAccount() {Balance = 23499m, BankName = "ОББ", SWIFTCode = "fjd98f349"},
                new BankAccount() {Balance = 99m, BankName = "БНБ", SWIFTCode = "fjd98f349"},
                new BankAccount() {Balance = 24499m, BankName = "UNCR", SWIFTCode = "fjd98f349"}
            };

            return bankAccounts;
        }
    }
}
