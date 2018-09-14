namespace CG.Recruitment.Sweepstake.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CG.Recruitment.Sweepstake.DataStore;
    using CG.Recruitment.Sweepstake.Library;
    using CG.Recruitment.Sweepstake.Library.Competitor;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route(StaticConstants.ApiCompetitors)]
    public class CompetitorController : Controller
    {
        private readonly CompetitorQueryHandler handler;

        public CompetitorController()
        {
            this.handler = new CompetitorQueryHandler();
        }

        [HttpGet(StaticConstants.All)]
        public async Task<IActionResult> All()
        {
            IEnumerable<Competitor> competitors = await this.handler.HandleAsync(new CompetitorQuery());
            return View(model: competitors.ToList());
        }
        
        [HttpGet]
        public async Task<string> Competitors()
        {
            IEnumerable<Competitor> competitorsCollection = await this.handler.HandleAsync(new CompetitorQuery());

            var competitors = competitorsCollection.ToList();
            
            string r = JsonConvert.SerializeObject(competitors, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            
            return r;
        }
        
        [HttpGet(StaticConstants.Competitor)]
        public async Task<IActionResult> Competitor(string name)
        {
            IEnumerable<Competitor> competitors = await this.handler.HandleAsync(new CompetitorQuery
            {
                Name = name
            });

            Competitor competitor = competitors.Where(c => c.Name == name).ToList().FirstOrDefault();

            if (competitor == null)
            {
                return Redirect(StaticConstants.ApiCompetitors);
            }
            
            return View(model:competitor);
        }
        
        [HttpPost(StaticConstants.Competitor)]
        public async Task<CreatedResult> Competition([FromBody] CreateCompetitorCommand command)
        {
            var handler = new CreateCompetitorHandler();

            await handler.HandleAsync(command);

            return this.Created(StaticConstants.Empty, new { message = StaticConstants.CompetitorCreatedSuccessMessage });
        }
    }
}