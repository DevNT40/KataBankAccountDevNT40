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

            //Test de l'US 2
            accountService.Withdraw(600);
            accountService.Deposit(200);
            accountService.Withdraw(100);

            //Test de l'US 3
            accountService.PrintStatement();

            System.Console.ReadKey();
        }
    }
}
