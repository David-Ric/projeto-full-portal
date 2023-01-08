using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        // public Titulo Titulo { get; set; }
        public string Grupo { get; set; }
        public string Ativo { get; set; }
        public string Descricao { get; set; }
        // public Funcao Funcao { get; set; }
        public string Funcao { get; set; }
        public bool Admin { get; set; }
        public bool Usuario { get; set; }
        public bool Comercial { get; set; }
        public bool Representante { get; set; }
        public string ImagemURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}