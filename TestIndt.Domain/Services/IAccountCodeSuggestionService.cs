
namespace TestUCondo.Domain.Services
{
    public interface IAccountCodeSuggestionService
    {
        string SugerirProximoCodigo(string paiAtual, Func<string, List<string>> buscarFilhosDiretos);
    }
}
