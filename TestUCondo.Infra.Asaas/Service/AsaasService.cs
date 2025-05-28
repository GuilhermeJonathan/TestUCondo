using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Web;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Customers;
using TestUCondo.Infra.Asaas.Interface;
using TestUCondo.Infra.Asaas.Models;

namespace TestUCondo.Infra.Asaas.Service
{
    public class AsaasService : IAsaasService
    {
        private readonly HttpClient _httpClient;
        private readonly AsaasSettings _settings;
        private readonly string route = "customers";

        public AsaasService(HttpClient httpClient, IOptions<AsaasSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("access_token", _settings.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "chave_teste");
        }

        public async Task<AsaasApiResultDTO<List<CustomerDTO>>> GetCustomersAsync(
            string? name = null,
            string? email = null,
            string? cpfCnpj = null,
            int? offset = null,
            int? limit = null
        )
        {
            try
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                if (!string.IsNullOrWhiteSpace(name)) query["name"] = name;
                if (!string.IsNullOrWhiteSpace(email)) query["email"] = email;
                if (!string.IsNullOrWhiteSpace(cpfCnpj)) query["cpfCnpj"] = cpfCnpj;
                if (offset.HasValue) query["offset"] = offset.Value.ToString();
                if (limit.HasValue) query["limit"] = limit.Value.ToString();

                var url = route;
                var queryString = query.ToString();
                if (!string.IsNullOrEmpty(queryString))
                    url += "?" + queryString;

                var response = await _httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Corrigido: deserializar corretamente para CustomerApiResult
                    var apiResult = JsonSerializer.Deserialize<CustomerApiResult>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); 

                    if (apiResult?.Data != null)
                        return AsaasApiResultDTO<List<CustomerDTO>>.SuccessResult(apiResult.Data, "Clientes obtidos com sucesso.");

                    return AsaasApiResultDTO<List<CustomerDTO>>.ErrorResult("Nenhum cliente encontrado.");
                }
                else
                {
                    return AsaasApiResultDTO<List<CustomerDTO>>.ErrorResult($"Erro ao buscar clientes: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                return AsaasApiResultDTO<List<CustomerDTO>>.ErrorResult($"Erro inesperado ao buscar clientes: {ex.Message}");
            }
        }

        public async Task<string> GetCustomerByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{route}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return $"Erro ao buscar cliente por ID: {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Erro inesperado ao buscar cliente por ID: {ex.Message}";
            }
        }

        public async Task<AsaasApiResultDTO<CustomerDTO>> CreateCustomerAsync(CustomerDTO customerDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(customerDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(route, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var createdCustomer = JsonSerializer.Deserialize<CustomerDTO>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return AsaasApiResultDTO<CustomerDTO>.SuccessResult(createdCustomer, "Cliente criado com sucesso.");
                }
                else
                {
                    return AsaasApiResultDTO<CustomerDTO>.ErrorResult($"Erro ao criar cliente: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                return AsaasApiResultDTO<CustomerDTO>.ErrorResult($"Erro inesperado ao criar cliente: {ex.Message}");
            }
        }
    }
}
