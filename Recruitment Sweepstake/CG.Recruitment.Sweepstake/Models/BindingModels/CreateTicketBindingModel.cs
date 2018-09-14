namespace CG.Recruitment.Sweepstake.Models.BindingModels
{
    public class CreateTicketBindingModel
    {
        public string competitor { get; set; }

        public string gambler { get; set; }

        public string competitionId { get; set; }

        public string competitionName { get; set; }

        public bool paymentReceived { get; set; } = false;
    }
}
