using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Idioma
    {
        public Idioma()
        {
            SkillIdioma = new HashSet<SkillIdioma>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<SkillIdioma> SkillIdioma { get; set; }
    }
}
