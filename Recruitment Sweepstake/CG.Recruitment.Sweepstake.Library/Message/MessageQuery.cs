// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.Library.Message
{
    using System;

    public class MessageQuery
    {
        public Guid Id { get; set; }

        public Guid FromGamblerId { get; set; }

        public Guid ToGamblerId { get; set; }
    }
}