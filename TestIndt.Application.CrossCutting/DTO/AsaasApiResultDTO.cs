namespace TestUCondo.Application.CrossCutting.DTO
{
    public class AsaasApiResultDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static AsaasApiResultDTO<T> SuccessResult(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message };

        public static AsaasApiResultDTO<T> ErrorResult(string message)
            => new() { Success = false, Message = message, Data = default };
    }
}