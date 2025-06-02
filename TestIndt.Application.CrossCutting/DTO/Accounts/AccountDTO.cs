using TestUCondo.Application.CrossCutting.Enum;

namespace TestUCondo.Application.CrossCutting.DTO.Accounts
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public AccountTypeEnum Tipo { get; set; }
        public bool AceitaLancamento { get; set; }
        public int? IdPai { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public List<AccountDTO> Filhos { get; set; } = new List<AccountDTO>();
    }
}
