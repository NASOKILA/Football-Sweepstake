namespace CG.Recruitment.Sweepstake.Tests.Handlers
{
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Ticket;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class TicketHandlerTests
    {
        private TicketQueryHandler handler { get; set; }

        private BuyTicketHandler buyTicketHandler { get; set; }

        private SweepstakeContext context { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            this.context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options);

            this.handler = new TicketQueryHandler();

            this.buyTicketHandler = new BuyTicketHandler();
        }

        [TestCleanup]
        public void AfterEachTest()
        { }

        [TestMethod]
        public async Task TicketsCount_ShouldReturnTicketsCount()
        {
            //Arrange
            var handler = new TicketQueryHandler();
            var ticketsFromHandler = await handler.HandleAsync(new TicketQuery());
            var tickets = ticketsFromHandler.ToList();

            //Act - do something
            var ticketsFromHandlerCount = ticketsFromHandler.ToList().Count;

            var ticketsFromDatabaseCount = this.context.Tickets.Count();

            //Assert - check results
            Assert.AreEqual(ticketsFromHandlerCount, ticketsFromDatabaseCount);
        }
        
        [TestMethod]
        public async Task BuyTicket_ShouldBuyTicket()
        {
            //Arrange
            var handler = new TicketQueryHandler();
            var ticketsFromHandler = await handler.HandleAsync(new TicketQuery());
            var ticketsFromHandlerCount = ticketsFromHandler.ToList().Count;

            var firstCompetitionFromDatabase = this.context.Competitions.FirstOrDefault();
            var firstCompetitorFromDatabase = this.context.Competitors.FirstOrDefault();
            var firstGamblerFromDatabase = this.context.Gamblers.FirstOrDefault();

            //Act - do something
            BuyTicketCommand command = new BuyTicketCommand
            {
                Id = Guid.NewGuid(),
                CompetitionId = firstCompetitionFromDatabase.Id,
                CompetitorId = firstCompetitorFromDatabase.Id,
                GamblerId = firstGamblerFromDatabase.Id,
                IsPaymentReceived = false
            };


            //Assert - check results
            try
            {
               await buyTicketHandler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}

