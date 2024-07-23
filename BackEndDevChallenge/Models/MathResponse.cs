namespace BackEndDevChallenge.Models
{
    public class MathResponse
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public double? Result { get; set; }

        public string? ErrorType { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
