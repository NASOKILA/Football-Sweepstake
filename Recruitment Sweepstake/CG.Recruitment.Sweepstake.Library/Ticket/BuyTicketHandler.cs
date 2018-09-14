// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Ticket
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class BuyTicketHandler
    {
        public async Task HandleAsync(BuyTicketCommand command)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            context.Tickets.AddAsync(new Ticket
            {
                Id = command.Id,
                CompetitionId = command.CompetitionId,
                CompetitorId = command.CompetitorId,
                GamblerId = command.GamblerId,
                IsPaymentReceived = command.IsPaymentReceived,
                BoughtAt = DateTime.Now,
                PaymentReceivedAt = command.IsPaymentReceived ? DateTime.UtcNow : null as DateTime?
            });
            context.SaveChangesAsync();
        }

        private Guid AssignCompetitor(BuyTicketCommand command)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);
            var competitors = context.Competitors.Where(c => c.CompetitionId == command.CompetitionId);
            return competitors.Skip(new Random().Next(0, competitors.Count())).First().Id;
        }
    }
}