using System;
using System.ComponentModel.DataAnnotations;
using CreditApplicationsTask.Models;
using CreditApplicationsTask.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CreditApplicationsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<ICreditDecisionService, CreditDecisionService>()
                .BuildServiceProvider();

            var creditDecisionService = serviceProvider.GetService<ICreditDecisionService>();

            Console.WriteLine("Welcome to the Credit Application Demo!");

            decimal creditAmount = ReadDecimalInput("Enter the credit amount: ");

            int term = ReadIntInput("Enter the term (repayment in months): ");

            decimal existingCreditAmount = ReadDecimalInput("Enter the current pre-existing credit amount (amount of debt the user has before applying for new credit): ");       

            var request = new CreditApplicationRequest
            {
                CreditAmount = creditAmount,
                Term = term,
                ExistingCreditAmount = existingCreditAmount
            };

            try
            {
                var response = creditDecisionService.MakeCreditDecision(request);
                Console.WriteLine($"Decision: {response.Decision}");
                Console.WriteLine($"Interest Rate: {response.InterestRate}%");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        static decimal ReadDecimalInput(string prompt)
        {
            decimal input;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
            return input;
        }

        static int ReadIntInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer");
            }
            return input;
        }
    }
}
