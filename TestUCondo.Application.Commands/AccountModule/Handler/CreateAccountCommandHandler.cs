using MediatR;
using AutoMapper;
using TestUCondo.Application.Commands.AccountModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Accounts;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.AccountModule.Handler
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ResultType>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResultType> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {  
            if(request.Pai != null)
            {
                var existePai = await GetPaiAsync(request.Pai.Value, cancellationToken);
                if (existePai is null)
                    return ResultType.ErrorResult(String.Format("Não existe um pai com o Id {0}.", request.Pai));

                if(request.Tipo != existePai.Tipo)
                    return ResultType.ErrorResult(String.Format("o Tipo da conta é diferente do tipo da conta do pai {0}.", existePai.Tipo));
                
                if(existePai.AceitaLancamento)
                    return ResultType.ErrorResult(String.Format("Esta conta pai ({0}) não pode ter contas filhas", existePai.Id));
            }

            var codigoDuplicado = await CodigoJaExisteAsync(request.Codigo, cancellationToken);
            if (codigoDuplicado)
                return ResultType.ErrorResult("Já existe uma conta com este código.");

            var account = new Account(request.Codigo, request.Descricao, request.Tipo,
                request.AceitaLancamento, request.Pai);

            await _accountRepository.AddAsync(account, cancellationToken);

            var accountDto = _mapper.Map<AccountDTO>(account);

            return ResultType.SuccessResult("Conta criada com sucesso.", accountDto);
        }

        #region Métodos Privados
        private async Task<Account> GetPaiAsync(long id, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAsync(a => a.Id == id, cancellationToken);
        }

        private async Task<bool> CodigoJaExisteAsync(string codigo, CancellationToken cancellationToken)
        {
            return await _accountRepository.ExistsAsync(a => a.Codigo == codigo, cancellationToken);
        }
        #endregion
    }
}