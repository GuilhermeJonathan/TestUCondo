using AutoMapper;
using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Customers;
using TestUCondo.Application.Queries.CustomerModule.Query;
using TestUCondo.Infra.Asaas.Interface;

namespace TestUCondo.Application.Queries.CustomerModule.Handler
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IAsaasService _asaasService;

        public GetCustomersQueryHandler(IMapper mapper, IAsaasService asaasService)
        {
            _mapper = mapper;
            _asaasService = asaasService;
        }

        public async Task<List<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var asaasResult = await _asaasService.GetCustomersAsync(
                request.Name,
                request.Email,
                request.CpfCnpj,
                request.Offset,
                request.Limit
            );

            if (!asaasResult.Success || asaasResult.Data == null)
            {
                // Optionally, log asaasResult.Message
                return new List<CustomerDTO>();
            }

            // If mapping is needed, do it here, otherwise just return asaasResult.Data
            return asaasResult.Data;
        }
    }
}