using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Endereco
    {
        public string Cep { get; set; }
        public string Pais { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int Idpessoa { get; set; }

        public virtual Pessoa IdpessoaNavigation { get; set; }
    }
}
