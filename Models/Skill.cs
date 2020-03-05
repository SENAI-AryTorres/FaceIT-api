using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Skill
    {
        public Skill()
        {
            PessoaSkill = new HashSet<PessoaSkill>();
            PropostaSkill = new HashSet<PropostaSkill>();
        }

        public int Idskill { get; set; }
        public int IdtipoSkill { get; set; }
        public string Descricao { get; set; }

        public virtual TipoSkill IdtipoSkillNavigation { get; set; }
        public virtual ICollection<PessoaSkill> PessoaSkill { get; set; }
        public virtual ICollection<PropostaSkill> PropostaSkill { get; set; }
    }
}
