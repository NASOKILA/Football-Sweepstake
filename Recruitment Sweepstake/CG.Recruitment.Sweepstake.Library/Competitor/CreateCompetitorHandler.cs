namespace CG.Recruitment.Sweepstake.Library.Competitor
{
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class CreateCompetitorHandler
    {
        public async Task HandleAsync(CreateCompetitorCommand command)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            context.Competitors.Add(new Competitor
            {
                Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id,
                CompetitionId = command.CompetitionId,
                Name = command.Name,
                Tickets = command.Tickets.ToList()
            });

            context.SaveChanges();
        }
    }
}
