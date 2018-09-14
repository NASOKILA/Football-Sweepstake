using System;
using System.Collections.Generic;
using System.Text;

namespace CG.Recruitment.Sweepstake.Library.Competitor
{
    public class CompetitorQuery
    {
        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public string Name { get; set; }
    }
}
