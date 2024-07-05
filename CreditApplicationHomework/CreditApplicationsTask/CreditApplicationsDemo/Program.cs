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
            // Set up a Dependency Injection Container
            var serviceProvider = new ServiceCollection()
                .AddScoped<ICreditDecisionService, CreditDecisionService>() // Register the ICreditDecisionService with the CreditDecisionService implementation.
                .BuildServiceProvider();                                    // Build the service provider to use the registered services

            // Retrieve an instance of the ICreditDecisionService from the DI container
            var creditDecisionService = serviceProvider.GetService<ICreditDecisionService>();

            Console.WriteLine("Welcome to the Credit Application Demo!");
            
            // Read the credit amount input from the user
            decimal creditAmount = ReadDecimalInput("Enter the credit amount: ");
            
            // Read the term input (repayment months_ from the user
            int term = ReadIntInput("Enter the term (repayment in months): ");
            
            // Read the exisiting credit amount input from the user
            decimal existingCreditAmount = ReadDecimalInput("Enter the current pre-existing credit amount (amount of debt the user has before applying for new credit): ");
            
            // Create a CreditApplicationRequest object with the user inputs
            var request = new CreditApplicationRequest
            {
                CreditAmount = creditAmount,
                Term = term,
                ExistingCreditAmount = existingCreditAmount
            };

            try
            {
                // Make a credit decision using the service and the request data
                var response = creditDecisionService.MakeCreditDecision(request);
                // Display the decision and the interest rate to the user
                Console.WriteLine($"Decision: {response.Decision}");
                Console.WriteLine($"Interest Rate: {response.InterestRate}%");
            }
            catch (ValidationException ex)
            {
                // Handle any validation errors thrown during the credit decision process
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            
            // Wait for user input before closing the application
            Console.ReadKey();
        }
        
        // Helper method to read a decimal input from the user with a prompt
        static decimal ReadDecimalInput(string prompt)
        {
            decimal input;
            while (true)
            {
                // Display prompt and read user input
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out input))
                {
                    break; // Exit the loop if input is invalid
                }
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
            return input;
        }
        
        // Helper method to read an integer input from the user with a prompt
        static int ReadIntInput(string prompt)
        {
            int input;
            while (true)
            {
                // Display prompt and read user input
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break; // Exit the loop if input is invalid
                }
                Console.WriteLine("Invalid input. Please enter a valid integer");
            }
            return input;
        }
    }
}
