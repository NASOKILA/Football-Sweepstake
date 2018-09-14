// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.DataStore
{
    using System;
    using System.Collections.Generic;

    public class Competition
    {
        public Competition()
        {
            this.Competitors = new List<Competitor>();

            this.Tickets = new List<Ticket>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal EntryFee { get; set; }

        public ICollection<Competitor> Competitors { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}