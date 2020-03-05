using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillDb
    {
        public int Idskill { get; set; }
        public int Iddb { get; set; }

        public virtual BancoDados IddbNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
