using MediatR;
using System.Collections.Generic;
using TestUCondo.Application.CrossCutting.DTO.Customers;

namespace TestUCondo.Application.Queries.CustomerModule.Query
{
    public class GetCustomersQuery : IRequest<List<CustomerDTO>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CpfCnpj { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }

        public GetCustomersQuery(
            string? name = null,
            string? email = null,
            string? cpfCnpj = null,
            int? offset = null,
            int? limit = null)
        {
            Name = name;
            Email = email;
            CpfCnpj = cpfCnpj;
            Offset = offset;
            Limit = limit;
        }

        public GetCustomersQuery() { }
    }
}