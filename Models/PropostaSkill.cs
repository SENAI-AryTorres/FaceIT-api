using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class PropostaSkill
    {
        public int Idproposta { get; set; }
        public int Idskill { get; set; }
        public int IdtipoSkill { get; set; }

        public virtual Skill Id { get; set; }
        public virtual Proposta IdpropostaNavigation { get; set; }
    }
}
