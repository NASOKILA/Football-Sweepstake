// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Message
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class MessageQueryHandler
    {
        public async Task<IEnumerable<Message>> HandleAsync(MessageQuery query)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            return context.Messages
                .Where(m => (query.Id == Guid.Empty || query.Id == m.Id) &&
                            (query.FromGamblerId == Guid.Empty || query.FromGamblerId == m.FromGamblerId) &&
                            (query.ToGamblerId == Guid.Empty || query.ToGamblerId == m.ToGamblerId))
                            .Include(m => m.FromGambler)
                            .Include(m => m.ToGambler);
        }
    }
}