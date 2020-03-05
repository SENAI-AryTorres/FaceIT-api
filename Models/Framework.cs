using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Framework
    {
        public Framework()
        {
            SkillFw = new HashSet<SkillFw>();
        }

        public int Id { get; set; }
        public string Fw { get; set; }

        public virtual ICollection<SkillFw> SkillFw { get; set; }
    }
}
