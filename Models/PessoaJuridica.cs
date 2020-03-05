using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class PessoaJuridica
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Ie { get; set; }
        public int Idpessoa { get; set; }

        public virtual Pessoa IdpessoaNavigation { get; set; }
    }
}
