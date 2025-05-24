using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUCondo.Application.CrossCutting.Enum;

namespace TestUCondo.Application.CrossCutting.DTO.Accounts
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public AccountTypeEnum Tipo { get; set; }
        public int? IdPai { get; set; }
    }
}
