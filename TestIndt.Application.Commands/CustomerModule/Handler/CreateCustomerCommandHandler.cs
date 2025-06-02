using MediatR;
using AutoMapper;
using TestUCondo.Application.Commands.CustomerModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Customers;
using TestUCondo.Infra.Asaas.Interface;

namespace TestUCondo.Application.Commands.CustomerModule.Handler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResultType>
    {
        private readonly IAsaasService _asaasService;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IAsaasService asaasService, IMapper mapper)
        {
            _asaasService = asaasService;
            _mapper = mapper;
        }

        public async Task<ResultType> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {            
            var customerDto = _mapper.Map<CustomerDTO>(request);
         
            var asaasResult = await _asaasService.CreateCustomerAsync(customerDto);

            if (!asaasResult.Success || asaasResult.Data == null)
                return ResultType.ErrorResult(asaasResult.Message ?? "Erro ao processar resposta da API Asaas.");

            return ResultType.SuccessResult(asaasResult.Message ?? "Cliente criado com sucesso.", asaasResult.Data);
        }
    }
}