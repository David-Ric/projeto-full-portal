using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class PermissoesDto
    {
        [Key]
        public int Id {get; set;}
        [Required(ErrorMessage ="O campo Nome é orbrigatório")]
        [MaxLength(30, ErrorMessage ="{0} deve ter no máximo 30 caracteres.")]
        public string Nome {get; set;}
    }
}