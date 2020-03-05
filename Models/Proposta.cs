using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Proposta
    {
        public Proposta()
        {
            Candidato = new HashSet<Candidato>();
            PropostaSkill = new HashSet<PropostaSkill>();
        }

        public int Idproposta { get; set; }
        public string Descricao { get; set; }
        public string TipoContrato { get; set; }
        public string Cidade { get; set; }
        public bool? Encerrada { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<Candidato> Candidato { get; set; }
        public virtual ICollection<PropostaSkill> PropostaSkill { get; set; }
    }
}
