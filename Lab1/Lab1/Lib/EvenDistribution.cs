namespace Lab1.Lib
{
    public class EvenDistributionHelper: DistributionHelper
    {
        private double _alpha;
        private double _sigma;
        private double _zeta;

        public EvenDistributionHelper(double alpha, double sigma, double zeta)
        {
            _alpha = alpha;
            _sigma = sigma;
            _zeta = zeta;
        }

        public override void GetDistribution(int size)
        {
            Distribution = new double[size];

            for (int i = 0; i < size; i++)
            {
                _zeta = _alpha * _zeta % _sigma;
                Distribution[i] = _zeta / _sigma;
            }
        }

        public override double GetDistributionValue(double x)
        {
            return x switch
            {
                < 0 => 0,
                >=0 and <= 1 => x,
                > 1 => 1
            };
        }
    }
}