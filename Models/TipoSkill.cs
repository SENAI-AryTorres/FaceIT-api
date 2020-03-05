using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class TipoSkill
    {
        public TipoSkill()
        {
            Skill = new HashSet<Skill>();
        }

        public int IdtipoSkill { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Skill> Skill { get; set; }
    }
}
