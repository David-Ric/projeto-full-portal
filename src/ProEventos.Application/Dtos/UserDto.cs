using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    public class UserDto
    {
         public string UserName { get; set; }
        public string Email { get; set; }
        public string Grupo { get; set; }
        public string Ativo { get; set; }
        public string Funcao { get; set; }
        public bool Admin { get; set; }
        public bool Usuario { get; set; }
        public bool Comercial { get; set; }
        public bool Representante { get; set; }
        public string Password { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
    }
}