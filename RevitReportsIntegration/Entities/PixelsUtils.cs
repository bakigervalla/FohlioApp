namespace Fohlio.RevitReportsIntegration.Entities
{
    public static class PixelsUtils
    {
        const double PixelsInInch = 96;
        const double FeetsInInch = 1 / 12.0;

        public static double ToFeets(double value) => value / PixelsInInch * FeetsInInch;
    }
}