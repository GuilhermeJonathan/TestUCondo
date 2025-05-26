namespace TestUCondo.Domain.Services
{
    public class AccountCodeSuggestionService : IAccountCodeSuggestionService
    {
        public string SugerirProximoCodigo(string paiAtual, Func<string, List<string>> buscarFilhosDiretos)
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
                    return proximoCodigo;
                }
                else
                {
                    // Subir um nível
                    var partes = paiAtual.Split('.').ToList();
                    if (partes.Count == 1)
                    {
                        int raiz = int.Parse(partes[0]) + 1;
                        return $"{raiz}.1";
                    }

                    partes.RemoveAt(partes.Count - 1);
                    var novoPai = string.Join('.', partes);

                    return SugerirProximoCodigo(novoPai, buscarFilhosDiretos);
                }
            }
        }
    }
}
