using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillIdioma
    {
        public int Idskill { get; set; }
        public int Ididioma { get; set; }

        public virtual Idioma IdidiomaNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
