// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Message
{
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using Microsoft.EntityFrameworkCore;

    public class SendMessageHandler
    {
        public async Task HandleAsync(SendMessageCommand command)
        {
            var context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                    .UseSqlServer(Constants.ConnectionString)
                    .Options);

            context.Messages.AddAsync(new Message
            {
                Id = command.Id,
                Subject = command.Subject,
                Body = command.Body,
                FromGamblerId = command.FromGamblerId,
                ToGamblerId = command.ToGamblerId
            });

            context.SaveChangesAsync();
        }
    }
}