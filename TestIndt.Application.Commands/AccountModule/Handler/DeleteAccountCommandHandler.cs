using MediatR;
using TestUCondo.Application.Commands.AccountModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.AccountModule.Handler
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ResultType>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResultType> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.Id, cancellationToken);
            if (account == null)
                return ResultType.ErrorResult("Conta não encontrada.");
            
            await RemoveChildrenAsync(account.Id, cancellationToken);

            await _accountRepository.DeleteAsync(account, cancellationToken);
            return ResultType.SuccessResult("Conta removida com sucesso.");
        }

        private async Task RemoveChildrenAsync(long parentId, CancellationToken cancellationToken)
        {            
            var children = await _accountRepository.GetListAsync(a => a.IdPai == parentId, cancellationToken);
            foreach (var child in children)
            {         
                await RemoveChildrenAsync(child.Id, cancellationToken);
                await _accountRepository.DeleteAsync(child, cancellationToken);
            }
        }
    }
}