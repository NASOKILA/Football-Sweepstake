// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Ticket
{
    using System;

    public class TicketQuery
    {
        public Guid Id { get; set; }
        
        public Guid CompetitionId { get; set; }

        public DataStore.Competition Competition { get; set; }

        public Guid CompetitorId { get; set; }

        public DataStore.Competitor Competitor { get; set; }
        
        public Guid GamblerId { get; set; }

        public DataStore.Gambler Gambler { get; set; }

        public DateRange? BoughtBetween { get; set; }

        public DateRange? PaidBetween { get; set; }
    }
}