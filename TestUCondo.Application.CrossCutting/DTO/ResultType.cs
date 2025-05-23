namespace TestUCondo.Application.CrossCutting.DTO
{
    public class ResultType
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
