﻿using System;
using System.Collections.Generic;

namespace faceitapi.Models
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Candidato = new HashSet<Candidato>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? Idskill { get; set; }
        public bool Excluido { get; set; }
        public int? GoogleId { get; set; }

        public virtual Skill IdskillNavigation { get; set; }
        public virtual Anexo Anexo { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual Imagem Imagem { get; set; }
        public virtual PessoaFisica PessoaFisica { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual ICollection<Candidato> Candidato { get; set; }
    }
}