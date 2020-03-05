using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class LinguagemProgramacao
    {
        public LinguagemProgramacao()
        {
            SkillLp = new HashSet<SkillLp>();
        }

        public int Id { get; set; }
        public string Linguagem { get; set; }

        public virtual ICollection<SkillLp> SkillLp { get; set; }
    }
}
