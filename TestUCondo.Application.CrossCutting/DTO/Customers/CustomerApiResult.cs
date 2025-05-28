using System.Collections.Generic;

namespace TestUCondo.Application.CrossCutting.DTO.Customers
{
    public class CustomerApiResult
    {
        public List<CustomerDTO>? Data { get; set; }
        // Adicione outras propriedades se necessário, como paging, totalCount, etc.
    }

    public class CustomerApiModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }         
        public string? CpfCnpj { get; set; }         
        public string? Phone { get; set; }         
    }
}