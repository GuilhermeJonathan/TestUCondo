using TestUCondo.Domain.Services;

namespace TestUCondo.Test
{
    public class AccountCodeSuggestionServiceTests
    {
        private readonly AccountCodeSuggestionService _service = new();

        [Fact]
        public void Deve_Sugerir_Primeiro_Filho_Quando_Nao_Ha_Filhos()
        {
            string pai = "1";
            List<string> filhos = new();
            string esperado = "1.1";
         
            var resultado = _service.SugerirProximoCodigo(pai, _ => filhos);

            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Deve_Sugerir_Proximo_Filho_Sequencial()
        {
            string pai = "1";
            List<string> filhos = new() { "1.1", "1.2", "1.3" };
            string esperado = "1.4";

            var resultado = _service.SugerirProximoCodigo(pai, _ => filhos);

            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Deve_Subir_Nivel_Quando_Limite_999_Atingido()
        {
            string pai = "1.2";
            List<string> filhos = new() { "1.2.999" };            
            List<string> filhosPai1 = new() { "1.1", "1.2" };

            var resultado = _service.SugerirProximoCodigo(pai, p =>
                p == "1.2" ? filhos :
                p == "1" ? filhosPai1 : new List<string>());

            Assert.Equal("1.3", resultado);
        }
    }
}