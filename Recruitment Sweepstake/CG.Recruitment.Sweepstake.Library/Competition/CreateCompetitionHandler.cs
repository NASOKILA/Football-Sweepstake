// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CG.Recruitment.Sweepstake.Library.Competition
{
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class CreateCompetitionHandler
    {
        public async Task HandleAsync(CreateCompetitionCommand command)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            context.Competitions.Add(new Competition
            {
                Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id,
                Name = command.Name,
                Description = command.Description,
                EntryFee = command.EntryFee,
                Competitors = command.CompetitorNames.Select(cn => new Competitor
                {
                    Id = Guid.NewGuid(),
                    Name = cn
                }).ToList()
            });
            context.SaveChanges();
        }
    }
}