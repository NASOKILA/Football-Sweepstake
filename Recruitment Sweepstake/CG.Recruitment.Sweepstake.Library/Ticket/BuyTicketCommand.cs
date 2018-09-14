// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Ticket
{
    using System;

    public class BuyTicketCommand
    {
        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public Guid CompetitorId { get; set; }

        public Guid GamblerId { get; set; }

        public bool IsPaymentReceived { get; set; }
    }
}