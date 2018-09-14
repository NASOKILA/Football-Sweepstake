namespace CG.Recruitment.Sweepstake.Tests.Handlers
{
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Competition;
    using CG.Recruitment.Sweepstake.Library.Competitor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class CompetitorHandlerTests
    {
        private CompetitorQueryHandler handler { get; set; }

        private CreateCompetitorHandler createCompetitorHandler { get; set; }

        private SweepstakeContext context { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            this.context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options);

            this.handler = new CompetitorQueryHandler();

            this.createCompetitorHandler = new CreateCompetitorHandler();
        }

        [TestCleanup]
        public void AfterEachTest()
        { }

        [TestMethod]
        public async Task CompetitorCount_ShouldReturnCompetitorCount()
        {
            //Arrange
            var handler = new CompetitorQueryHandler();
            var competitorsFromHandler = await handler.HandleAsync(new CompetitorQuery());

            //Act - do something
            var competitorsFromHandlerCount = competitorsFromHandler.ToList().Count;

            var competitorsFromDatabaseCount = this.context.Competitors.Count();

            //Assert - check results
            Assert.AreEqual(competitorsFromHandlerCount, competitorsFromDatabaseCount);
        }

        [TestMethod]
        public async Task RegisterCompetitor_ShouldRegisterCompetitor()
        {
            //Arrange
            var handler = new CompetitorQueryHandler();
            var competitorsFromHandler = await handler.HandleAsync(new CompetitorQuery());
            var competitorsFromHandlerCount = competitorsFromHandler.ToList().Count;
            var firstCompetitionFromDatabase = this.context.Competitions.FirstOrDefault();

            //Act - do something
            CreateCompetitorCommand command = new CreateCompetitorCommand
            {
                Id = Guid.NewGuid(),
                Tickets = new List<Ticket>(),
                Name = "Competitor Unit Test",
                CompetitionId = firstCompetitionFromDatabase.Id
            };

            await createCompetitorHandler.HandleAsync(command);

            var competitorsFromDatabaseCount = this.context.Competitors.Count();

            Assert.AreNotEqual(competitorsFromDatabaseCount, competitorsFromHandlerCount);
        }
    }
}

