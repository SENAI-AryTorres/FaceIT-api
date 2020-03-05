using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Pessoa = new HashSet<Pessoa>();
            Proposta = new HashSet<Proposta>();
            SkillDb = new HashSet<SkillDb>();
            SkillFw = new HashSet<SkillFw>();
            SkillIde = new HashSet<SkillIde>();
            SkillIdioma = new HashSet<SkillIdioma>();
            SkillLp = new HashSet<SkillLp>();
            SkillPlataforma = new HashSet<SkillPlataforma>();
            SkillVersionamento = new HashSet<SkillVersionamento>();
        }

        public int Id { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
        public virtual ICollection<Proposta> Proposta { get; set; }
        public virtual ICollection<SkillDb> SkillDb { get; set; }
        public virtual ICollection<SkillFw> SkillFw { get; set; }
        public virtual ICollection<SkillIde> SkillIde { get; set; }
        public virtual ICollection<SkillIdioma> SkillIdioma { get; set; }
        public virtual ICollection<SkillLp> SkillLp { get; set; }
        public virtual ICollection<SkillPlataforma> SkillPlataforma { get; set; }
        public virtual ICollection<SkillVersionamento> SkillVersionamento { get; set; }
    }
}
