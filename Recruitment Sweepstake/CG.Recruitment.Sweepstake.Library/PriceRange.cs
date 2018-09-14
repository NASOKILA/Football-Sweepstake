// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library
{
    public struct PriceRange
    {
        public readonly decimal From;
        public readonly decimal To;

        public PriceRange(decimal from, decimal to)
        {
            this.From = from;
            this.To = to;
        }
    }
}