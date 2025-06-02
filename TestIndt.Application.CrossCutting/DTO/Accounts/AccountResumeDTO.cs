namespace TestUCondo.Application.CrossCutting.DTO.Accounts
{
    public class AccountResumeDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string DescricaoCodigo => $"{Codigo} - {Descricao}";
        public List<AccountResumeDTO> Filhos { get; set; } = new List<AccountResumeDTO>();
    }
}
