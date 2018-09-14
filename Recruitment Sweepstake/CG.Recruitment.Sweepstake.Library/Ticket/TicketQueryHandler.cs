// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class TicketQueryHandler
    {
        public async Task<IEnumerable<Ticket>> HandleAsync(TicketQuery query)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            var tickets = context
                .Tickets
                .Where(t => query.Id == Guid.Empty || t.Id == query.Id)
                .Include(t => t.Competition)
                .Include(t => t.Gambler)
                .Include(t => t.Competitor);

            if (query.BoughtBetween.HasValue)
            {
                tickets = tickets
                    .Where(t => t.BoughtAt <= query.BoughtBetween.Value.From &&
                                t.BoughtAt >= query.BoughtBetween.Value.To)
                                .Include(t => t.Competition)
                                .Include(t => t.Gambler)
                                .Include(t => t.Competitor); 
            }

            if (query.PaidBetween.HasValue)
            {
                tickets = tickets
                    .Where(t => t.PaymentReceivedAt >= query.PaidBetween.Value.From &&
                                t.PaymentReceivedAt <= query.PaidBetween.Value.To)
                                .Include(t => t.Competition)
                                .Include(t => t.Gambler)
                                .Include(t => t.Competitor); 
            }

            if (query.CompetitionId != Guid.Empty)
            {
                tickets = tickets.Where(t => t.CompetitionId == query.CompetitionId)
                    .Include(t => t.Competition)
                    .Include(t => t.Gambler)
                    .Include(t => t.Competitor); 
            }

            if (query.GamblerId != Guid.Empty)
            {
                tickets = tickets.Where(t => t.GamblerId == query.GamblerId)
                    .Include(t => t.Competition)
                    .Include(t => t.Gambler)
                    .Include(t => t.Competitor); 
            }

            if (query.CompetitorId != Guid.Empty)
            {
                tickets = tickets.Where(t => t.CompetitorId == query.CompetitorId)
                    .Include(t => t.Competition)
                    .Include(t => t.Gambler)
                    .Include(t => t.Competitor);
            }

            return tickets;
        }
    }
}