using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillLp
    {
        public int Idskill { get; set; }
        public int Idlp { get; set; }

        public virtual LinguagemProgramacao IdlpNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
