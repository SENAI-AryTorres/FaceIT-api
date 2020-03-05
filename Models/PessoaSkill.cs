using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class PessoaSkill
    {
        public int Idpessoa { get; set; }
        public int Idskill { get; set; }
        public int IdtipoSkill { get; set; }

        public virtual Skill Id { get; set; }
        public virtual Pessoa IdpessoaNavigation { get; set; }
    }
}
