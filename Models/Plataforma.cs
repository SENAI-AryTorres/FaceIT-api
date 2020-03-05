using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Plataforma
    {
        public Plataforma()
        {
            SkillPlataforma = new HashSet<SkillPlataforma>();
        }

        public int Id { get; set; }
        public string Plataforma1 { get; set; }

        public virtual ICollection<SkillPlataforma> SkillPlataforma { get; set; }
    }
}
