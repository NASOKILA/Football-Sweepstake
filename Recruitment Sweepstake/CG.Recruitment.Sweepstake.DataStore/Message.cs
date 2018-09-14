// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.DataStore
{
    using System;

    public class Message
    {
        public Guid Id { get; set; }

        public Guid FromGamblerId { get; set; }

        public Gambler FromGambler { get; set; }

        public Guid ToGamblerId { get; set; }

        public Gambler ToGambler { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}