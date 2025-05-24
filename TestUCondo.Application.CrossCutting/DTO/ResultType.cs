namespace TestUCondo.Application.CrossCutting.DTO
{
    public class ResultType
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static ResultType SuccessResult(string message, object? data = null)
        {
            return new ResultType
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ResultType ErrorResult(string message, object? data = null)
        {
            return new ResultType
            {
                Success = false,
                Message = message,
                Data = data
            };
        }
    }
}
