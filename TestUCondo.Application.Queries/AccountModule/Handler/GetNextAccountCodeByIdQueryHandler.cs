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

            var rootPrefix = account.Codigo.Split('.').First();
            var filhosRaiz = await _accountRepository.GetAllByPrefixAsync(rootPrefix, cancellationToken);

            var resultado = SugerirProximoCodigo(account.Codigo, (pai) =>
            {
                return filhosRaiz
                    .Where(a => a.Codigo.StartsWith(pai + "."))
                    .Where(a => a.Codigo.Split('.').Length == pai.Split('.').Length + 1)
                    .Select(a => a.Codigo)
                    .ToList();
            });

            return ResultType.SuccessResult("Código criado com sucesso.", resultado.ProximoCodigo);
        }

        public static ResultadoSugestao SugerirProximoCodigo(string paiAtual,
            Func<string, List<string>> buscarFilhosDiretos)
        {
            const int maxLevels = 5;

            while (true)
            {
                var nivelAtual = paiAtual.Split('.').Length;
                var filhos = buscarFilhosDiretos(paiAtual);

                var filhosNumeros = filhos
                    .Select(c =>
                    {
                        var partesPai = paiAtual.Split('.').Length;
                        var partes = c.Split('.');
                        return partes.Length == partesPai + 1 ? int.Parse(partes[partesPai]) : -1;
                    })
                    .Where(n => n != -1)
                    .OrderBy(n => n)
                    .ToList();

                if ((filhosNumeros.Count == 0 || filhosNumeros.Last() < 999) && nivelAtual < maxLevels)
                {
                    int proximo = filhosNumeros.Count == 0 ? 1 : filhosNumeros.Last() + 1;
                    string proximoCodigo = $"{paiAtual}.{proximo}";
                    return new ResultadoSugestao
                    {
                        ProximoCodigo = proximoCodigo
                    };
                }
                else
                {
                    // Subir um nível
                    var partes = paiAtual.Split('.').ToList();
                    if (partes.Count == 1)
                    {
                        int raiz = int.Parse(partes[0]) + 1;
                        return new ResultadoSugestao
                        {
                            ProximoCodigo = $"{raiz}.1"
                        };
                    }

                    partes.RemoveAt(partes.Count - 1);
                    var novoPai = string.Join('.', partes);

                    return SugerirProximoCodigo(novoPai, buscarFilhosDiretos);
                }
            }
        }

        public class ResultadoSugestao
        {
            public string ProximoCodigo { get; set; }
        }
    }
}