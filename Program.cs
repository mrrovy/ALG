using System;
using ALG.Algorithms;  // Add this to reference the Algorithm classes

namespace AdvancedAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Advanced Algorithms Showcase!");

            bool keepRunning = true;
            while (keepRunning)
            {
                int choice = DisplayMenu();
                ExecuteAlgorithm(choice);
            }
        }

        static int DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select an algorithm to execute:");
            Console.WriteLine("1. P vs NP Solver");
            Console.WriteLine("2. Inverse-flux Algorithms");
            Console.WriteLine("3. Search Algorithms");
            Console.WriteLine("4. Algorithmic Information Theory");  // Added this line
            Console.WriteLine("0. Exit");
            Console.Write("\nValue: ");

            int choice = int.Parse(Console.ReadLine());
            return choice;
        }


        static void ExecuteAlgorithm(int choice)
        {
            switch (choice)
            {
                case 1:  // Added this case
                    Console.Clear();
                    Console.WriteLine("Enter lower bound integer:");
                    int lowerBound = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter upper bound integer:");
                    int upperBound = int.Parse(Console.ReadLine());
                    bool isVerified = PvsNPSolver.Execute(lowerBound, upperBound);
                    Console.WriteLine($"Verification result: {isVerified}");
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                    break;
                case 2:
                    // Execute and display Inverse-flux Algorithms
                    Console.Clear();
                    break;
                case 3:
                    // Execute and display Search Algorithms
                    Console.Clear();
                    ExecuteSearchAlgorithms();
                    break;
                case 4:  // Added this new case for Algorithmic Information Theory
                    Console.Clear();
                    Console.WriteLine("Enter a string to analyze:");
                    string inputString = Console.ReadLine();
                    AlgorithmicInfoTheory.ComplexMetricsReport(inputString);
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                    break;
                    break;
                case 0:
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void ExecuteSearchAlgorithms()
        {
            Console.WriteLine("Select a search algorithm:");
            Console.WriteLine("1. Linear Search");
            Console.WriteLine("2. Binary Search");
            Console.WriteLine("3. Jump Search");
            Console.WriteLine("4. Interpolation Search");
            Console.WriteLine("5. Exponential Search");
            int searchChoice = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a comma-separated list of integers:");
            string[] inputStrings = Console.ReadLine().Split(',');
            int[] arr = Array.ConvertAll(inputStrings, int.Parse);

            Console.WriteLine("Enter the target integer:");
            int target = int.Parse(Console.ReadLine());

            int result = -1;

            switch (searchChoice)
            {
                case 1:
                    result = SearchAlgorithms.LinearSearch(arr, target);
                    break;
                case 2:
                    result = SearchAlgorithms.BinarySearch(arr, target);
                    break;
                case 3:
                    result = SearchAlgorithms.JumpSearch(arr, target);
                    break;
                case 4:
                    result = SearchAlgorithms.InterpolationSearch(arr, target);
                    break;
                case 5:
                    result = SearchAlgorithms.ExponentialSearch(arr, target);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to main menu.");
                    return;
            }

            if (result != -1)
            {
                Console.WriteLine($"Element found at index {result}.");
            }
            else
            {
                Console.WriteLine("Element not found.");
            }
        }

        static void ExitProgram()
        {
            Console.WriteLine("Thank you for exploring the Advanced Algorithms Showcase. Goodbye!");
            Environment.Exit(0);
        }
    }
}
