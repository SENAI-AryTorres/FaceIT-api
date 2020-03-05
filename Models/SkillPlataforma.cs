using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillPlataforma
    {
        public int Idskill { get; set; }
        public int Idplataforma { get; set; }

        public virtual Plataforma IdplataformaNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
