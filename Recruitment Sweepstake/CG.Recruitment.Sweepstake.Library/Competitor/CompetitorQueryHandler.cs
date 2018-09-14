namespace CG.Recruitment.Sweepstake.Library.Competitor
{
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompetitorQueryHandler
    {
        public async Task<IEnumerable<Competitor>> HandleAsync(CompetitorQuery query)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            return context.Competitors
                .Where(g => (query.Id == Guid.Empty || g.Id == query.Id) &&
                                               (query.CompetitionId == Guid.Empty || g.CompetitionId == query.CompetitionId) &&
                                               (query.Name == null || query.Name == "" || g.Name.ToLowerInvariant().Contains(query.Name.ToLowerInvariant())))
                                               .Include(c => c.Tickets)
                                               .ThenInclude(c => c.Gambler)
                                               .Include(c => c.Competition);
                                               
        }
    }
}
