using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class PessoaFisica
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public int Idpessoa { get; set; }

        public virtual Pessoa IdpessoaNavigation { get; set; }
    }
}
