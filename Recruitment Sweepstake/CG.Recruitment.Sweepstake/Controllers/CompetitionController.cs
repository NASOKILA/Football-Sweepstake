// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Competition;
    using CG.Recruitment.Sweepstake.Library.Competitor;
    using CG.Recruitment.Sweepstake.Library.Gambler;
    using CG.Recruitment.Sweepstake.Models.BindingModels;
    using Microsoft.AspNetCore.Mvc;

    [Route(StaticConstants.ApiCompetitions)]
    public class CompetitionController : Controller
    {
        private readonly CompetitionQueryHandler handler;
        
        public CompetitionController()
        {
            this.handler = new CompetitionQueryHandler();
        }

        [HttpGet(StaticConstants.All)]
        public async Task<IActionResult> Competitions()
        {
            IEnumerable<Competition> competitions =  await this.handler.HandleAsync(new CompetitionQuery());
            return View(model:competitions.ToList());
        }

        [HttpGet(StaticConstants.Competition)]
        public async Task<IActionResult> Competition(CompetitionBindingModel competitionBindingModel)
        {
            ViewData[StaticConstants.Error] = competitionBindingModel.error;

            IEnumerable<Competition> competitions = await this.handler.HandleAsync(new CompetitionQuery {
                Name = competitionBindingModel.name
            });

            Competition competition = competitions.Where(c => c.Name == competitionBindingModel.name).ToList().FirstOrDefault();

            if (competition == null)
            {
                return Redirect(StaticConstants.ApiCompetitions);
            }

            IEnumerable<Competitor> competitors = await new CompetitorQueryHandler().HandleAsync(new CompetitorQuery());

            competition.Competitors = competitors.Where(c => c.CompetitionId == competition.Id).ToList();

            IEnumerable<Gambler> gamblers = await new GamblerQueryHandler().HandleAsync(new GamblerQuery());
            
            ViewData[StaticConstants.Gamblers] = gamblers.ToList();

            return View(model:competition);
        }

        [HttpPost(StaticConstants.Competition)]
        public async Task<IActionResult> Competition([FromBody] CreateCompetitionCommand command)
        {
            var handler = new CreateCompetitionHandler();
            await handler.HandleAsync(command);
            return this.Created(StaticConstants.Empty, new { message = StaticConstants.CompetitionCreatedSuccessMessage });
        }
    }
}