using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Imagem
    {
        public int Idpessoa { get; set; }
        public string Nome { get; set; }
        public byte[] Bytes { get; set; }

        public virtual Pessoa IdpessoaNavigation { get; set; }
    }
}
