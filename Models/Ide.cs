using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Ide
    {
        public Ide()
        {
            SkillIde = new HashSet<SkillIde>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<SkillIde> SkillIde { get; set; }
    }
}
