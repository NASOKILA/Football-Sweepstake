// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.DataStore
{
    using System;

    public class Ticket
    {
        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public Competition Competition { get; set; }
        
        public Guid CompetitorId { get; set; }

        public Competitor Competitor { get; set; }
        
        public Guid GamblerId { get; set; }

        public Gambler Gambler { get; set; }

        public DateTime BoughtAt { get; set; }

        public bool IsPaymentReceived { get; set; }

        public DateTime? PaymentReceivedAt { get; set; }
    }
}