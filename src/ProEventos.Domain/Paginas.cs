using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Paginas
    {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public IEnumerable<Grupos> Grupo {get; set;}
    }
}