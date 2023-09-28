using System;
using System.Linq;

namespace Lab1.Lib
{
    public class ExponentialDistributionHelper: DistributionHelper
    {
        private double _lambda;

        public ExponentialDistributionHelper(double lambda)
        {
            _lambda = lambda;
        }

        public override void GetDistribution(int size)
        {
            Distribution = new double[size];
            double coef = -1 / _lambda;
            Random random = new Random();
        
            for (int i = 0; i < size; i++)
            {
                Distribution[i] = coef * Math.Log(random.NextDouble());
            }
        }

        public override double GetDistributionValue(double x)
        {
            return 1 - Math.Exp(-_lambda * x);
        }
    }
}