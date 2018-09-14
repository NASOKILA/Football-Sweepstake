// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Competition
{
    using System;
    using System.Collections.Generic;
    using CG.Recruitment.Sweepstake.DataStore;

    public class CreateCompetitionCommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Description { get; set; }

        public int EntryFee { get; set; }

        public IEnumerable<string> CompetitorNames { get; set; }
    }

}