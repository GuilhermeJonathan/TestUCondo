using AutoMapper;
using MediatR;
using TestUCondo.Application.Commands.AccountModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Accounts;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.AccountModule.Handler
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ResultType>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResultType> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(a => a.Id == request.Id, cancellationToken);
            if (account == null)
                return ResultType.ErrorResult("Conta não encontrada.");

            if (request.Pai.HasValue)
            {
                var existePai = await GetPaiAsync(request.Pai.Value, cancellationToken);
                if (existePai is null)
                    return ResultType.ErrorResult($"Não existe um pai com o Id {request.Pai}.");

                if (request.Tipo != existePai.Tipo)
                    return ResultType.ErrorResult($"O tipo da conta é diferente do tipo da conta do pai {existePai.Tipo}.");

                if (existePai.AceitaLancamento)
                    return ResultType.ErrorResult($"Esta conta pai ({existePai.Id}) não pode ter contas filhas.");
            }

            if (!account.Codigo.Equals(request.Codigo))
            {
                var codigoDuplicado = await CodigoJaExisteAsync(request.Codigo, cancellationToken);
                if (codigoDuplicado)
                    return ResultType.ErrorResult("Já existe uma conta com este código.");
            }

            account.SetUpdate(request.Codigo, request.Descricao, request.Tipo, request.AceitaLancamento, request.Pai);

            await _accountRepository.UpdateAsync(account, cancellationToken);

            var accountDto = _mapper.Map<AccountDTO>(account);
            return ResultType.SuccessResult("Conta atualizada com sucesso.", accountDto);
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