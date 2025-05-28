using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestUCondo.Infra.Asaas.Interface
{
    public interface IAsaasService
    {
        Task<string> GetCustomerByIdAsync(string id);

        Task<AsaasApiResultDTO<List<CustomerDTO>>> GetCustomersAsync(
            string? name = null,
            string? email = null,
            string? cpfCnpj = null,
            int? offset = null,
            int? limit = null
        );

        Task<AsaasApiResultDTO<CustomerDTO>> CreateCustomerAsync(CustomerDTO customerDto);
    }
}
