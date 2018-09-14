// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class CompetitionQueryHandler
    {
        public async Task<IEnumerable<Competition>> HandleAsync(CompetitionQuery query)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            return context.Competitions
                .Where(c => (query.Id == Guid.Empty || query.Id == c.Id) &&
                            ((query.Name ?? "") == "" ||
                             c.Name.ToLowerInvariant().Contains(query.Name.ToLowerInvariant())) &&
                            (query.CompetitorIds == null || !query.CompetitorIds.Any() || query.CompetitorIds.Any(id => id == c.Id)) &&
                            query.EntryFeeBetween == null ||
                            !query.EntryFeeBetween.HasValue ||
                             query.EntryFeeBetween.Value.From <= c.EntryFee &&
                             query.EntryFeeBetween.Value.To >= c.EntryFee)
                             .Include(c => c.Tickets)
                             .Include(c => c.Competitors);
        }
    }
}