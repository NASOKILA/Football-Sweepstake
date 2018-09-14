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

    [Route(StaticConstants.ApiGamblers)]
    public class GamblerController : Controller
    {
        [HttpGet(StaticConstants.All)]
        public async Task<IActionResult> Gamblers(string error)
        {
            IEnumerable<Gambler> gamblers = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());

            ViewData[StaticConstants.Gamblers] = gamblers.ToList();
            ViewData[StaticConstants.Error] = error;

            return View(model:gamblers.ToList());
        }

        [HttpGet(StaticConstants.Gambler)]
        public async Task<IActionResult> Gambler(GamblerBindingModel gamblerBindingModel)
        {
            ViewData[StaticConstants.Message] = gamblerBindingModel.message;
            ViewData[StaticConstants.Error] = gamblerBindingModel.error;

            var currentGambler = await new GamblerQueryHandler().HandleAsync(new GamblerQuery
            {
                Name = gamblerBindingModel.name
            });

            IEnumerable<Gambler> gamblers = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());

            Gambler gambler = currentGambler.ToList().FirstOrDefault();
            
            if (gambler == null)
            {
                return RedirectToAction(StaticConstants.GamblersAll, new { error = StaticConstants.GamblerDontExistError });
            }

            var messagesSendByGambler = await new MessageQueryHandler().HandleAsync(new MessageQuery());
            var messagesSend = messagesSendByGambler.Where(m => m.FromGamblerId == gambler.Id).ToList();
            ViewData[StaticConstants.MessagesSend] = messagesSend;

            var messagesReceivedByGambler = await new MessageQueryHandler().HandleAsync(new MessageQuery());
            var messagesReceived = messagesReceivedByGambler.Where(m => m.ToGamblerId == gambler.Id).ToList();
            ViewData[StaticConstants.MessagesReceived] = messagesReceived;
            
            return View(model:gambler);
        }

        [HttpPost(StaticConstants.Gambler)]
        public async Task<IActionResult> Gambler(Guid id, string name)
        { 
            if (name == null)
            {
                return RedirectToAction(StaticConstants.All, StaticConstants.ApiGamblers, new { error = StaticConstants.GamblerNameIsRequired });
            }
            
            
            var handler = new CreateGamblerHandler();

            CreateGamblerCommand command = new CreateGamblerCommand
            {
                Id = id,
                Name = name
            };

            await handler.HandleAsync(command);

            return RedirectToAction(StaticConstants.Gambler, new { name = command.Name, message = StaticConstants.GamblerCreated });
        }
    }
}
