using Lab1.Lib;

namespace Lab1
{
    public static class Program
    {
        private static int SIZE = 10000;

        [STAThread]
        public static void Main()
        {
            NormalDistributionHelper distributionHelper = new NormalDistributionHelper(alpha: 0, sigma: 5);
            
            SetUpDistribution(distributionHelper);
        }

        public static void SetUpDistribution(DistributionHelper distributionHelper)
        {
            distributionHelper.GetDistribution(SIZE);

            double mean = distributionHelper.GetMean();
            double variation = distributionHelper.GetVariation();

            Console.WriteLine($"Mean: {mean}, Variation:{variation}");

            var freq = distributionHelper.GetFrequencies(100);
            
            double chiSquared = distributionHelper.RunHypothesisTest();
            Console.WriteLine($"X^2 = {chiSquared}");
            
            distributionHelper.ShowPlot(distributionHelper.Distribution);
        }

    }
}