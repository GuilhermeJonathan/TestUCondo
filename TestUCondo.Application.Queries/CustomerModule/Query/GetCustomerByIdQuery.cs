using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Customers;

namespace TestUCondo.Application.Queries.CustomerModule.Query
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO?>
    {
        public string Id { get; set; }
        public GetCustomerByIdQuery(string id)
        {
            Id = id;
        }
    }
}