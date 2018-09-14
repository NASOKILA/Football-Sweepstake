namespace CG.Recruitment.Sweepstake.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Competitor;
    using CG.Recruitment.Sweepstake.Library.Gambler;
    using CG.Recruitment.Sweepstake.Library.Ticket;
    using CG.Recruitment.Sweepstake.Models.BindingModels;
    using Microsoft.AspNetCore.Mvc;

    [Route(StaticConstants.ApiTickets)]
    public class TicketController : Controller
    {
        [HttpGet(StaticConstants.All)]
        public async Task<IActionResult> Tickets()
        {
            var handler = new TicketQueryHandler();
            var ticketsFromHandler = await handler.HandleAsync(new TicketQuery());
            var tickets = ticketsFromHandler.ToList();
            return View(model: tickets);
        }
        
        [HttpGet(StaticConstants.Ticket)]
        public async Task<IActionResult> Ticket(string id, string message, string error)
        {
            ViewData[StaticConstants.Message] = message;
            ViewData[StaticConstants.Error] = error;

            var currentTicket = await new TicketQueryHandler().HandleAsync(new TicketQuery
            {
                Id = Guid.Parse(id)
            });

            Ticket ticket = currentTicket.ToList().FirstOrDefault();

            if (ticket == null)
            {
                return Redirect(StaticConstants.ApiGamblers);
            }

            return View(model: ticket);
        }
        
        [HttpPost(StaticConstants.Ticket)]
        public async Task<IActionResult> Ticket(CreateTicketBindingModel createTicketBindingModel)
        {
            //get competitor ID
            IEnumerable<Competitor> competitors = await new CompetitorQueryHandler().HandleAsync(new CompetitorQuery
            {
                Name = createTicketBindingModel.competitor
            });

            Competitor competitor = competitors.Where(c => c.Name == createTicketBindingModel.competitor)
                .ToList()
                .FirstOrDefault();

            if (competitor == null)
            {
                return RedirectToAction(StaticConstants.Competition, StaticConstants.ApiCompetitions, new { name = createTicketBindingModel.competitionName, error = StaticConstants.TicketNotCreatedCreatorNullError });
            }

            Guid competitorId = competitor.Id;


            //get gambler ID
            var currentGambler = await new GamblerQueryHandler().HandleAsync(new GamblerQuery
            {
                Name = createTicketBindingModel.gambler
            });

            Gambler gambler = currentGambler.Where(c => c.Name == createTicketBindingModel.gambler)
                .ToList()
                .FirstOrDefault();

            if (gambler == null)
            {
                return RedirectToAction(StaticConstants.Competition, StaticConstants.ApiCompetitions, new { name = createTicketBindingModel.competitionName, error = StaticConstants.TicketNotCreatedGamblerNullError });
            }

            Guid gamblerId = gambler.Id;


            //Create BuyTicketCommand command
            BuyTicketCommand command = new BuyTicketCommand
            {
                Id = Guid.NewGuid(),
                CompetitionId = Guid.Parse(createTicketBindingModel.competitionId),
                CompetitorId = competitorId,
                GamblerId = gamblerId,
                IsPaymentReceived = createTicketBindingModel.paymentReceived
            };

            var handler = new BuyTicketHandler();
            await handler.HandleAsync(command);
            
            return RedirectToAction(StaticConstants.Ticket, new { id = command.Id, message = StaticConstants.TicketBoughtSuccessMessage });
        }
    }
}