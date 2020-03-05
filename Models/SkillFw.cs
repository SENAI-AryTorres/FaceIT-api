using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillFw
    {
        public int Idskill { get; set; }
        public int Idfw { get; set; }

        public virtual Framework IdfwNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
