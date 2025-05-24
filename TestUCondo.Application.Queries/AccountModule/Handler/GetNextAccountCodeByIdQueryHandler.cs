using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.Queries.AccountModule.Query;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Queries.AccountModule.Handler
{
    public class GetNextAccountCodeByIdQueryHandler : IRequestHandler<GetNextAccountCodeByIdQuery, ResultType>
    {
        private readonly IAccountRepository _accountRepository;

        public GetNextAccountCodeByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResultType> Handle(GetNextAccountCodeByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.Id, cancellationToken);
            if (account == null)
                return ResultType.ErrorResult(String.Format("Não existe conta com o Id {0}.", request.Id));
            
            string nextCode = IncrementNextLevelByAccount(account);
            string newNextCode = IncrementNextLevel(GetLastChildCode(account));

            return ResultType.SuccessResult("Código criado com sucesso.", nextCode);
        }

        private static string GetLastChildCode(Domain.Entities.Account account)
        {
            if (account.Filhos == null || !account.Filhos.Any())
                return null;

            var prefix = account.Codigo + ".";
            var directChildren = account.Filhos
                .Where(f => f.Codigo.StartsWith(prefix) && f.Codigo.Count(c => c == '.') == prefix.Count(c => c == '.'))
                .ToList();

            if (!directChildren.Any())
                return null;
            
            var lastChild = directChildren
                .Select(f => new { Codigo = f.Codigo, Suffix = int.TryParse(f.Codigo.Split('.').Last(), out var s) ? s : 0 })
                .OrderByDescending(x => x.Suffix)
                .First();

            return lastChild.Codigo;
        }

        private static string IncrementNextLevelByAccount(Domain.Entities.Account account)
        {
            string nextCode;
            int nextSuffix;

            if (account.Filhos != null && account.Filhos.Any())
            {
                var prefix = account.Codigo + ".";
                var directChildren = account.Filhos
                    .Where(f => f.Codigo.StartsWith(prefix) && f.Codigo.Count(c => c == '.') == prefix.Count(c => c == '.'))
                    .ToList();

                int maxSuffix = 0;
                string maxCode = null;

                foreach (var filho in directChildren)
                {
                    var parts = filho.Codigo.Split('.');
                    if (int.TryParse(parts.Last(), out int suffix))
                    {
                        if (suffix > maxSuffix)
                        {
                            maxSuffix = suffix;
                            maxCode = filho.Codigo;
                        }
                    }
                }

                nextSuffix = maxSuffix + 1;
                if (nextSuffix > 999 && maxCode != null)
                {
                    var parts = maxCode.Split('.').ToList();
                    if (parts.Count >= 2 && int.TryParse(parts[^2], out int penultimo))
                    {
                        parts[^2] = (penultimo + 1).ToString();
                        parts[^1] = "1";
                        nextCode = string.Join('.', parts);
                    }
                    else
                    {
                        nextCode = $"{account.Codigo}.0";
                    }
                }
                else
                {
                    nextCode = $"{account.Codigo}.{nextSuffix}";
                }
            }
            else
            {
                nextSuffix = 1;
                nextCode = $"{account.Codigo}.{nextSuffix}";
            }

            return nextCode;
        }

        private string IncrementNextLevel(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return "1";

            var parts = codigo.Split('.').ToList();
            
            if (int.TryParse(parts.Last(), out int last))
            {
                if (last < 999)
                {
                    parts[^1] = (last + 1).ToString();
                }
                else
                {                    
                    if (parts.Count >= 2 && int.TryParse(parts[^2], out int penultimo))
                    {
                        parts[^2] = (penultimo + 1).ToString();
                        parts[^1] = "1";
                    }
                    else
                    {
                        parts.Add("1");
                    }
                }
            }
            else
            {
                parts.Add("1");
            }

            return string.Join('.', parts);
        }
    }
}