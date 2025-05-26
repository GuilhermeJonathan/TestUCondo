using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.Queries.AccountModule.Query;
using TestUCondo.Domain.Entities.Repositories;
using TestUCondo.Domain.Services;

namespace TestUCondo.Application.Queries.AccountModule.Handler
{
    public class GetNextAccountCodeByIdQueryHandler : IRequestHandler<GetNextAccountCodeByIdQuery, ResultType>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountCodeSuggestionService _accountCodeSuggestionService;

        public GetNextAccountCodeByIdQueryHandler(IAccountRepository accountRepository, IAccountCodeSuggestionService accountCodeSuggestionService)
        {
            _accountRepository = accountRepository;
            _accountCodeSuggestionService = accountCodeSuggestionService;
        }

        public async Task<ResultType> Handle(GetNextAccountCodeByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.Id, cancellationToken);
            if (account == null)
                return ResultType.ErrorResult(String.Format("Não existe conta com o Id {0}.", request.Id));

            var rootPrefix = account.Codigo.Split('.').First();
            var filhosRaiz = await _accountRepository.GetAllByPrefixAsync(rootPrefix, cancellationToken);

            var resultado = _accountCodeSuggestionService.SugerirProximoCodigo(account.Codigo, (pai) =>
            {
                return filhosRaiz
                    .Where(a => a.Codigo.StartsWith(pai + "."))
                    .Where(a => a.Codigo.Split('.').Length == pai.Split('.').Length + 1)
                    .Select(a => a.Codigo)
                    .ToList();
            });

            return ResultType.SuccessResult("Código criado com sucesso.", resultado);
        }
    }
}