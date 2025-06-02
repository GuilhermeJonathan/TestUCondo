using System.ComponentModel.DataAnnotations;

namespace TestUCondo.Domain.Entities
{
    public class EntidadeBase
    {
        public EntidadeBase()
        {
            DataCadastro = DateTime.Now;
            DataAlteracao = DateTime.Now;
        }

        public EntidadeBase(long id, DateTime dataCadastro, DateTime dataAlteracao)
        {
            Id = id;
            DataCadastro = dataCadastro;
            DataAlteracao = dataAlteracao;
        }

        [Key]
        [Required]
        public long Id { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
        public DateTime DataAlteracao { get; protected set; }
    }
}
