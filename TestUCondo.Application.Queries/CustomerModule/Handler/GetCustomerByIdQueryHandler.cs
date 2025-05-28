using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Customers;
using TestUCondo.Application.Queries.CustomerModule.Query;
using AutoMapper;
using TestUCondo.Infra.Asaas.Interface;
using System.Text.Json;

namespace TestUCondo.Application.Queries.CustomerModule.Handler
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO?>
    {
        private readonly IMapper _mapper;
        private readonly IAsaasService _asaasService;

        public GetCustomerByIdQueryHandler(IMapper mapper, IAsaasService asaasService)
        {
            _mapper = mapper;
            _asaasService = asaasService;
        }

        public async Task<CustomerDTO?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var resultCustomer = await _asaasService.GetCustomerByIdAsync(request.Id);
            if (string.IsNullOrEmpty(resultCustomer))
            {
                return null;
            }

            var apiModel = JsonSerializer.Deserialize<CustomerApiModel>(resultCustomer, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiModel == null)
                return null;

            return _mapper.Map<CustomerDTO>(apiModel);
        }
    }
}