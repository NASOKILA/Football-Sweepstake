// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.DataStore
{
    using System;
    using System.Collections.Generic;

    public class Competitor
    {
        public Competitor()
        {
            this.Tickets = new List<Ticket>();
        }

        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}