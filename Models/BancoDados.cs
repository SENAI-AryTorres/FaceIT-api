using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class BancoDados
    {
        public BancoDados()
        {
            SkillDb = new HashSet<SkillDb>();
        }

        public int Id { get; set; }
        public string Db { get; set; }

        public virtual ICollection<SkillDb> SkillDb { get; set; }
    }
}
