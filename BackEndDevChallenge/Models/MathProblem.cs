namespace BackEndDevChallenge.Models
{
    public class MathProblem
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public double? Result { get; set; }

        public MathOperationType OperationType { get; set; }

        public string? ErrorMessage { get; set; }

        public ErrorType ErrorType { get; set; }

        public DateTime Timestamp { get; set; }
    }
    
}
