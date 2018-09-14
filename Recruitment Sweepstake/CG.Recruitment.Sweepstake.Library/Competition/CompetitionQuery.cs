// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Competition
{
    using System;
    using System.Collections.Generic;

    public class CompetitionQuery
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PriceRange? EntryFeeBetween { get; set; }

        public IEnumerable<Guid> CompetitorIds { get; set; }
    }
}