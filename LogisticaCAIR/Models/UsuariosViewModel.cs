using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Models
{
    public class UsuariosViewModel
    {
        [Required]
        [StringLength(70, ErrorMessage ="Valor permitido: entre 20 y 70 caracteres", MinimumLength =20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Valor permitido: entre 8 y 20 caracteres", MinimumLength = 8)]        
        public string UserName { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "Valor permitido: entre 8 y 13 caracteres", MinimumLength = 8)]
        public int Pass { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Registrar un correo electrónico con un formato valido")]
        public string Email { get; set; }
        [Required]
        public int Perfil { get; set; }
    }
}
