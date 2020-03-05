using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Candidato
    {
        public int Idproposta { get; set; }
        public int Idpessoa { get; set; }

        public virtual Pessoa IdpessoaNavigation { get; set; }
        public virtual Proposta IdpropostaNavigation { get; set; }
    }
}
