namespace CG.Recruitment.Sweepstake.Tests.Handlers
{
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Message;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class MessageHandlersTests
    {
        private MessageQueryHandler handler { get; set; }

        private SendMessageHandler sendMessageHandler { get; set; }

        private SweepstakeContext context { get; set; }
        
        [TestInitialize]
        public void BeforeEachTest()
        {
            this.context = new SweepstakeContext(
                new DbContextOptionsBuilder<SweepstakeContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options);

            this.handler = new MessageQueryHandler();

            this.sendMessageHandler = new SendMessageHandler();
        }

        [TestCleanup]
        public void AfterEachTest()
        {}

        [TestMethod]
        public async Task MessagesCount_ShouldReturnMessageCount()
        {
            //Arrange
            var handler = new MessageQueryHandler();
            var messagesFromHandler = await handler.HandleAsync(new MessageQuery());
            var messages = messagesFromHandler.ToList();

            //Act - do something
            var messagesFromHandlerCount = messagesFromHandler.ToList().Count;

            var messagesFromDatabaseCount = this.context.Messages.Count();

            //Assert - check results
            Assert.AreEqual(messagesFromHandlerCount, messagesFromDatabaseCount);
        }

        [TestMethod]
        public async Task SendMessage_ShouldSendMessage()
        {
            //Arrange
            var handler = new MessageQueryHandler();
            var messagesFromHandler = await handler.HandleAsync(new MessageQuery());
            var messagesFromHandlerCount = messagesFromHandler.ToList().Count;
            
            var firstGamblerFromDatabase = this.context.Gamblers.FirstOrDefault();
            var lastGamblerFromDatabase = this.context.Gamblers.LastOrDefault();

            //Act - do something

            string body = "Test body!";
            string subject = "Test subject!";

            SendMessageCommand command = new SendMessageCommand
            {
                Body = body,
                Subject = subject,
                Id = Guid.NewGuid(),
                FromGamblerId = firstGamblerFromDatabase.Id, //sender id
                ToGamblerId = lastGamblerFromDatabase.Id // receiver id
            };
            
            //Assert - check results
            await this.sendMessageHandler.HandleAsync(command);

            var messagesFromDatabaseCount = this.context.Tickets.Count();

            Assert.AreNotEqual(messagesFromDatabaseCount, messagesFromHandlerCount);
            
        }
    }
}
