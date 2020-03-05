using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Versionamento
    {
        public Versionamento()
        {
            SkillVersionamento = new HashSet<SkillVersionamento>();
        }

        public int Id { get; set; }
        public string Ferramenta { get; set; }

        public virtual ICollection<SkillVersionamento> SkillVersionamento { get; set; }
    }
}
