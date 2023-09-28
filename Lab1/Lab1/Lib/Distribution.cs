using ScottPlot;
using ScottPlot.Statistics;

namespace Lab1.Lib
{
    public abstract class DistributionHelper
    {
        internal double[] Distribution;
        internal double Mean;
        internal double Variation;
        internal double SegmentLength;
        internal int SegmentCounts;
        internal double[] Frequencies;
        public abstract void GetDistribution(int size);

        public double GetMean()
        {
            Mean = 0;

            foreach (var x in Distribution)
            {
                Mean += x;
            }
            Mean /= Distribution.Length;
            
            return Mean;
        }

        public double GetVariation()
        {
            double sumOfSquares = 0;
            foreach (var x in Distribution)
            {
                sumOfSquares += x*x;
            }

            Variation = sumOfSquares / Distribution.Length - Mean * Mean;
            return Variation;
        }

        public double[] GetFrequencies(int segmentsCount)
        {
            SegmentCounts = segmentsCount;
            Frequencies = new double[segmentsCount];
            double min = Distribution.Min();
            SegmentLength = (Distribution.Max() - min) / segmentsCount;

            foreach (var x in Distribution)
            {
                int segment = (int)((x - min) / SegmentLength);
                if (segment == segmentsCount) segment--;
                Frequencies[segment]++;
            }

            return Frequencies;
        }
        
        public void ShowPlot(double[] numbers)
        {
            double min = numbers.Min();
            double max = numbers.Max();

            Plot plot = new();
            Histogram hist = new(min, max, SegmentCounts);

            hist.AddRange(numbers);

            var bar = plot.AddBar(values: hist.Counts, positions: hist.Bins);
            bar.BarWidth = (max - min) / hist.BinCount;
        
            WpfPlotViewer viewer = new(plot);
            viewer.ShowDialog();
        }

        public double RunHypothesisTest()
        {
            double min = Distribution.Min();
            double chiSquared = 0;
            int segmentStartId = 0;
            double frequency = 0;
            int intervalsCount = 0;
            for (int i = 0; i < Frequencies.Length; i++)
            {
                frequency += Frequencies[i];
                if (frequency < 5)
                {
                    continue;
                }
                double expected = GetDistributionValue((i + 1) * SegmentLength + min) -
                                  GetDistributionValue(segmentStartId * SegmentLength + min);
                expected *= Distribution.Length;
                chiSquared += Math.Pow(frequency - expected, 2) / expected;
                intervalsCount++;
                
                segmentStartId = i+1;
                frequency = 0;
            }

            if (segmentStartId != Frequencies.Length)
            {
                double expected = GetDistributionValue(Frequencies.Length * SegmentLength + min) -
                                   GetDistributionValue(segmentStartId * SegmentLength + min);
                expected *= Distribution.Length;
                chiSquared += Math.Pow(frequency - expected, 2) / expected;
                intervalsCount++;
            }

            Console.WriteLine($"Intervals: {intervalsCount}");

            return chiSquared;
        }

        public abstract double GetDistributionValue(double x);
    }
}