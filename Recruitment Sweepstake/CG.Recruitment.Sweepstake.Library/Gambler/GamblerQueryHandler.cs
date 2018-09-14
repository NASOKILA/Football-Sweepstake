// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Gambler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class GamblerQueryHandler
    {
        public async Task<IEnumerable<Gambler>> HandleAsync(GamblerQuery query)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            return context.Gamblers.Where(g => (query.Id == Guid.Empty || g.Id == query.Id) &&
                                               (query.Name == null || query.Name == "" || g.Name.ToLowerInvariant().Contains(query.Name.ToLowerInvariant())));
        }
    }
}