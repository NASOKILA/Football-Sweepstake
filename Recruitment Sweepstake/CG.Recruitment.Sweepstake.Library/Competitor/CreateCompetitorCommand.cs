namespace CG.Recruitment.Sweepstake.Library.Competitor
{
    using CG.Recruitment.Sweepstake.DataStore;
    using System;
    using System.Collections.Generic;

    public class CreateCompetitorCommand
    {
        public Guid Id { get; set; } = Guid.Empty;

        public Guid CompetitionId { get; set; }

        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
