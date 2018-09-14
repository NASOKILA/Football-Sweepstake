// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library
{
    using System;

    public struct DateRange
    {
        public readonly DateTime From; 
        public readonly DateTime To;

        public DateRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }
    }
}