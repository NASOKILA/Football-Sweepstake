namespace CG.Recruitment.Sweepstake.Library.Message
{
    using System;

    public class SendMessageCommand
    {
        public Guid Id { get; set; }

        public Guid FromGamblerId { get; set; }

        public Guid ToGamblerId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
