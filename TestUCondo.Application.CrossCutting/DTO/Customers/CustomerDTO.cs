using System.Text.Json.Serialization;

namespace TestUCondo.Application.CrossCutting.DTO.Customers
{
    public class CustomerDTO
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("cpfCnpj")]
        public string? CpfCnpj { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}