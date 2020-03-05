using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillVersionamento
    {
        public int Idskill { get; set; }
        public int Idversionamento { get; set; }

        public virtual Skill IdskillNavigation { get; set; }
        public virtual Versionamento IdversionamentoNavigation { get; set; }
    }
}
