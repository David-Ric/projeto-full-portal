using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProEventos.Domain;

namespace ProEventos.Application.Dtos
{
    public class PaginaDto
    {
        [Key]
        public int Id {get; set;}
        [Required(ErrorMessage ="O campo Nome é orbrigatório")]
        [MaxLength(30, ErrorMessage ="{0} deve ter no máximo 30 caracteres.")]
        public string Nome {get; set;}
        public IEnumerable<Grupos> Grupos { get; set; }
    }
}