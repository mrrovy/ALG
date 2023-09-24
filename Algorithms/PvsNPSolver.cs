using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ALG.Algorithms
{
    public class PvsNPSolver
    {
        public static bool Execute(int lowerBound, int upperBound, bool useSimulatedAnnealing = false)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[INFO] Generating Problem Space...");
            Console.ResetColor();
            List<int> problemSpace = GenerateProblemSpace(lowerBound, upperBound);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[INFO] Solving TSP...");
            Console.ResetColor();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<int> tspSolution = useSimulatedAnnealing ? SolveTSPWithSimulatedAnnealing(problemSpace) : SolveTSP(problemSpace);
            sw.Stop();
            long solveTime = sw.ElapsedMilliseconds;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[STAT] Time to solve: {solveTime} ms");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[INFO] Verifying TSP Solution...");
            Console.ResetColor();
            sw.Restart();
            bool isVerified = VerifyTSP(tspSolution, problemSpace);
            sw.Stop();
            long verifyTime = sw.ElapsedMilliseconds;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[STAT] Time to verify: {verifyTime} ms");
            Console.ResetColor();

            Console.ForegroundColor = isVerified ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"[RESULT] Verification status: {isVerified}");
            Console.ResetColor();

            return isVerified;
        }


        static List<int> GenerateProblemSpace(int lowerBound, int upperBound)
        {
            Random rand = new Random();
            return Enumerable.Range(0, upperBound - lowerBound + 1)
                             .Select(x => rand.Next(lowerBound, upperBound))
                             .ToList();
        }

        static List<int> SolveTSP(List<int> problemSpace)
        {
            List<int> bestRoute = null;
            int shortestDistance = int.MaxValue;

            foreach (var permutation in GetPermutations(problemSpace, problemSpace.Count))
            {
                int distance = CalculateTotalDistance((List<int>)permutation);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestRoute = permutation.ToList();
                }
            }

            return bestRoute;
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            var items = list.ToList();
            var retList = new List<List<T>>();
            var workList = new List<T>(items);

            for (int i = 0; i < Math.Pow(items.Count, length); i++)
            {
                var result = new List<T>();

                for (int j = 0; j < length; j++)
                {
                    result.Add(workList[i % workList.Count]);
                    i /= workList.Count;
                }

                retList.Add(result);
                workList = new List<T>(items);
            }

            return retList;
        }


        static int CalculateTotalDistance(List<int> route)
        {
            int totalDistance = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                totalDistance += Math.Abs(route[i] - route[i + 1]);
            }
            return totalDistance;
        }

        static bool VerifyTSP(List<int> solution, List<int> problemSpace)
        {
            int totalDistance = CalculateTotalDistance(solution);
            return totalDistance < int.MaxValue && !solution.Except(problemSpace).Any() && !problemSpace.Except(solution).Any();
        }

        static List<int> SolveTSPWithSimulatedAnnealing(List<int> problemSpace)
        {
            List<int> bestRoute = new List<int>(problemSpace);
            int bestDistance = CalculateTotalDistance(bestRoute);
            double temperature = 100.0;
            double coolingRate = 0.995;

            Random rand = new Random();

            while (temperature > 1)
            {
                List<int> newRoute = new List<int>(bestRoute);
                int pos1 = rand.Next(newRoute.Count);
                int pos2 = rand.Next(newRoute.Count);

                // Swap positions
                int temp = newRoute[pos1];
                newRoute[pos1] = newRoute[pos2];
                newRoute[pos2] = temp;

                int newDistance = CalculateTotalDistance(newRoute);

                // Calculate acceptance probability
                double acceptanceProbability = Math.Exp((bestDistance - newDistance) / temperature);

                if (acceptanceProbability > rand.NextDouble())
                {
                    bestRoute = newRoute;
                    bestDistance = newDistance;
                }

                temperature *= coolingRate;
            }

            return bestRoute;
        }
    }
}
