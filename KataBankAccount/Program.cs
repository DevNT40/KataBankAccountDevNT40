using System;

namespace KataBankAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountService = new AccountService(
                new TransactionRepository(new DateTimeProvider()),
                new StatementPrinter(new Console()));

            //Test de l'US 1
            accountService.Deposit(1000);
            accountService.Deposit(500);
            accountService.Deposit(200);

            accountService.PrintStatement();
            System.Console.ReadKey();
        }
    }
}
