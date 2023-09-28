namespace Lab1.Lib
{
    public class NormalDistributionHelper: DistributionHelper
    {
        private double _sigma;
        private double _alpha;

        public NormalDistributionHelper(double sigma, double alpha)
        {
            _sigma = sigma;
            _alpha = alpha;
        }

        public override void GetDistribution(int size)
        {
            Distribution = new double[size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                double mu = -6;
                for (int j = 0; j < 12; j++)
                {
                    mu += random.NextDouble();
                }

                Distribution[i] = _sigma * mu + _alpha;
            }
        }

        public override double GetDistributionValue(double x)
        {
            return (1 + Erf((x - _alpha) / (_sigma * Math.Sqrt(2)))) / 2;
        }
        
        private double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;
 
            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);
 
            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p*x);
            double y = 1.0 - (((((a5*t + a4)*t) + a3)*t + a2)*t + a1)*t*Math.Exp(-x*x);
 
            return sign*y;
        }
    }
}