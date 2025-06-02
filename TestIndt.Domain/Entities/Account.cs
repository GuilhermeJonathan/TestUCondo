using System.ComponentModel.DataAnnotations.Schema;
using TestUCondo.Application.CrossCutting.Enum;

namespace TestUCondo.Domain.Entities
{
    public class Account : EntidadeBase
    {
        public Account() : base()
        {
            Ativo = true;
        }

        public Account(string codigo, string descricao, AccountTypeEnum tipo, bool aceitaLancamento,
            long? idPai = null) : this()
        {
            Codigo = codigo;
            Descricao = descricao;
            Tipo = tipo;
            AceitaLancamento = aceitaLancamento;
            IdPai = idPai;
        }

        public string Codigo { get; set; }
        public string? Descricao { get; set; }
        public bool AceitaLancamento { get; set; }
        public AccountTypeEnum Tipo { get; set; }
        public bool Ativo { get; set; }

        public long? IdPai { get; set; }
        [ForeignKey(nameof(IdPai))]
        public virtual Account? Pai { get; set; }
        public virtual ICollection<Account> Filhos { get; set; } = new List<Account>();

        public void SetUpdate(string codigo, string descricao, AccountTypeEnum tipo, bool aceitaLancamento,
            long? idPai = null)
        {
            Codigo = codigo;
            Descricao = descricao;
            Tipo = tipo;
            AceitaLancamento = aceitaLancamento;
            IdPai = idPai;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}
