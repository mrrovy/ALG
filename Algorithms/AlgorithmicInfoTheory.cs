using System.IO.Compression;
using System.Text;

namespace ALG.Algorithms
{
    public class AlgorithmicInfoTheory
    {
        public static double CalculateShannonEntropy(string inputString)
        {
            var frequencyDict = new Dictionary<char, int>();
            foreach (char c in inputString)
            {
                if (frequencyDict.ContainsKey(c))
                {
                    frequencyDict[c]++;
                }
                else
                {
                    frequencyDict[c] = 1;
                }
            }

            int strLen = inputString.Length;
            double entropy = 0.0;
            foreach (var entry in frequencyDict)
            {
                double frequency = (double)entry.Value / strLen;
                entropy -= frequency * (Math.Log(frequency) / Math.Log(2));
            }

            return entropy;
        }

        public static int EstimateKolmogorovComplexity(string inputString)
        {
            byte[] inputData = Encoding.UTF8.GetBytes(inputString);
            MemoryStream outputStream = new MemoryStream();

            using (GZipStream gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gZipStream.Write(inputData, 0, inputData.Length);
            }

            byte[] compressedData = outputStream.ToArray();
            return compressedData.Length;
        }

        public static int CalculateLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.IsNullOrEmpty(t) ? 0 : t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int[] costs = new int[t.Length + 1];
            for (int i = 0; i <= s.Length; i++)
            {
                int previousCost = i;
                for (int j = 0; j <= t.Length; j++)
                {
                    if (i == 0)
                    {
                        costs[j] = j;
                    }
                    else
                    {
                        if (j > 0)
                        {
                            int currentCost = costs[j];
                            costs[j] = Math.Min(Math.Min(costs[j] + 1, previousCost + 1),
                                                costs[j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1));
                            previousCost = currentCost;
                        }
                    }
                }
            }
            return costs[t.Length];
        }

        public static double RelativeComplexity(string string1, string string2)
        {
            int complexity1 = EstimateKolmogorovComplexity(string1);
            int complexity2 = EstimateKolmogorovComplexity(string2);
            return (double)complexity1 / complexity2;
        }

        public static bool IsRandom(string inputString, double threshold = 0.9)
        {
            double entropy = CalculateShannonEntropy(inputString);
            double maxEntropy = Math.Log(inputString.Length) / Math.Log(2);
            return (entropy / maxEntropy) > threshold;
        }

        public static void EmpiricalAnalysis(List<string> dataSet)
        {
            foreach (var data in dataSet)
            {
                Console.WriteLine($"Data: {data}, Estimated Complexity: {EstimateKolmogorovComplexity(data)}");
            }
        }


        public static void ComplexMetricsReport(string inputString)
        {
            Console.WriteLine("======= Algorithmic Information Theory Metrics =======");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Shannon Entropy: {CalculateShannonEntropy(inputString)}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Estimated Kolmogorov Complexity: {EstimateKolmogorovComplexity(inputString)}");
            Console.ResetColor();

            // Here, we simply calculate the Levenshtein distance between the string and its reverse as an illustrative example.
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Levenshtein Distance (compared to its reverse): {CalculateLevenshteinDistance(inputString, new string(inputString.Reverse().ToArray()))}");
            Console.ResetColor();
            Console.WriteLine("======================================================");
        }
    }
}
