using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class SkillIde
    {
        public int Idskill { get; set; }
        public int Idide { get; set; }

        public virtual Ide IdideNavigation { get; set; }
        public virtual Skill IdskillNavigation { get; set; }
    }
}
