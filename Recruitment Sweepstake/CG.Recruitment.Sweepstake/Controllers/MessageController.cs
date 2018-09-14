namespace CG.Recruitment.Sweepstake.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Gambler;
    using CG.Recruitment.Sweepstake.Library.Message;
    using CG.Recruitment.Sweepstake.Models.BindingModels;
    using Microsoft.AspNetCore.Mvc;

    [Route(StaticConstants.ApiMessages)]
    public class MessageController : Controller
    {
        [HttpGet(StaticConstants.All)]
        public async Task<IActionResult> Messages(string message, string error)
        {
            ViewData[StaticConstants.Message] = message;
            ViewData[StaticConstants.Error] = error;

            var handler = new MessageQueryHandler();

            var messagesFromHandler = await handler.HandleAsync(new MessageQuery());

            //get all gamblers
            IEnumerable<Gambler> gamblersList = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());
            
            var messages = messagesFromHandler.ToList();

            foreach (Message m in messages)
            {
                m.ToGambler = gamblersList.ToList().FirstOrDefault(g => g.Id == m.ToGamblerId);
            }


            IEnumerable<Gambler> gamblers = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());
            
            return View(model: messages);
        }

        [HttpGet(StaticConstants.SendMessage)]
        public async Task<IActionResult> SendMessage()
        {
            IEnumerable<Gambler> gamblers = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());

            ViewData[StaticConstants.Gamblers] = gamblers.ToList();

            return View();
        }

        [HttpPost("Message")]
        public async Task<IActionResult> Message(MessageBindingModel messageBindingModel)
        {
            //get sender ID
            var senderGambler = await new GamblerQueryHandler().HandleAsync(new GamblerQuery
            {
                Name = messageBindingModel.sender
            });
      
            Gambler sender = senderGambler.ToList().FirstOrDefault(g => g.Name == messageBindingModel.sender);

            if (sender == null)
            {
                return RedirectToAction(StaticConstants.All, StaticConstants.ApiMessages, new { error = StaticConstants.SenderDontExistError });   
            }
            
            //get receiver ID
            var receiverGambler = await new GamblerQueryHandler().HandleAsync(new GamblerQuery
            {
                Name = messageBindingModel.receiver
            });
            
            Gambler receiver = receiverGambler.ToList().FirstOrDefault(g => g.Name == messageBindingModel.receiver);

            if (receiver == null)
            {
                return RedirectToAction(StaticConstants.All, StaticConstants.ApiMessages, new { error = StaticConstants.ReceiverDontExistError });
            }

            SendMessageCommand command = new SendMessageCommand {
                Body = messageBindingModel.body,
                Subject = messageBindingModel.subject,
                Id = Guid.NewGuid(),
                FromGamblerId = sender.Id,
                ToGamblerId = receiver.Id
            };

            var handler = new SendMessageHandler();
            await handler.HandleAsync(command);
            
            return RedirectToAction(StaticConstants.All, StaticConstants.ApiMessages, new { message = StaticConstants.MessageSentSuccessMessage });
        }
    }
}
