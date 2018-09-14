namespace CG.Recruitment.Sweepstake.Tests.Handlers
{
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Gambler;
    using CG.Recruitment.Sweepstake.Library.Ticket;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class GamblerHandlerTests
    {
        private GamblerQueryHandler handler { get; set; }

        private CreateGamblerHandler createGamblerHandler { get; set; }

        private SweepstakeContext context { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            this.context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options);

            this.handler = new GamblerQueryHandler();

            this.createGamblerHandler = new CreateGamblerHandler();
        }

        [TestCleanup]
        public void AfterEachTest()
        { }

        [TestMethod]
        public async Task GamblersCount_ShouldReturnGamblersCount()
        {
            //Arrange
            var handler = new GamblerQueryHandler();
            var gamblersFromHandler = await handler.HandleAsync(new GamblerQuery());
            
            //Act - do something
            var gamblersFromHandlerCount = gamblersFromHandler.ToList().Count;

            var gamblersFromDatabaseCount = this.context.Gamblers.Count();

            //Assert - check results
            Assert.AreEqual(gamblersFromHandlerCount, gamblersFromDatabaseCount);
        }

        [TestMethod]
        public async Task RegisterGambler_ShouldRegisterGambler()
        {
            //Arrange
            var handler = new TicketQueryHandler();
            var gamblersFromHandler = await handler.HandleAsync(new TicketQuery());
            var gamblersFromHandlerCount = gamblersFromHandler.ToList().Count;
            
            //Act - do something
            
            CreateGamblerCommand command = new CreateGamblerCommand
            {
                Id = Guid.NewGuid(),
                Name = "Mikey"
            };

            await createGamblerHandler.HandleAsync(command);

            var gamblersFromDatabaseCount = this.context.Gamblers.Count();

            Assert.AreNotEqual(gamblersFromDatabaseCount, gamblersFromHandlerCount);
        }
    }
}

