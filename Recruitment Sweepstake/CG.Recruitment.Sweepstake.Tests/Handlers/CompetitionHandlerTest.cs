namespace CG.Recruitment.Sweepstake.Tests.Handlers
{
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Competition;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class CompetitionHandlerTests
    {
        private CompetitionQueryHandler handler { get; set; }

        private CreateCompetitionHandler createCompetitionHandler { get; set; }

        private SweepstakeContext context { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            this.context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options);

            this.handler = new CompetitionQueryHandler();

            this.createCompetitionHandler = new CreateCompetitionHandler();
        }

        [TestCleanup]
        public void AfterEachTest()
        { }

        [TestMethod]
        public async Task CompetitionsCount_ShouldReturnCompetitionsCount()
        {
            //Arrange
            var handler = new CompetitionQueryHandler();
            var competitionsFromHandler = await handler.HandleAsync(new CompetitionQuery());
            
            //Act - do something
            var competitionsFromHandlerCount = competitionsFromHandler.ToList().Count;

            var competitionsFromDatabaseCount = this.context.Competitions.Count();

            //Assert - check results
            Assert.AreEqual(competitionsFromHandlerCount, competitionsFromDatabaseCount);
        }

        [TestMethod]
        public async Task RegisterCompetition_ShouldRegisterCompetition()
        {
            //Arrange
            var handler = new CompetitionQueryHandler();
            var competitionsFromHandler = await handler.HandleAsync(new CompetitionQuery());
            var competitionsFromHandlerCount = competitionsFromHandler.ToList().Count;
            
            //Act - do something
            CreateCompetitionCommand command = new CreateCompetitionCommand
            {
                Id = Guid.NewGuid(),
                Description = "Test description from unit test.",
                CompetitorNames = new List<string>(),
                Name = "Competition Unit Test",
                EntryFee = 10
            };

            await createCompetitionHandler.HandleAsync(command);

            var competitionsFromDatabaseCount = this.context.Competitions.Count();

            Assert.AreNotEqual(competitionsFromDatabaseCount, competitionsFromHandlerCount);
        }
    }
}

