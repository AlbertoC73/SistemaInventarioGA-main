using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.Modelos
{
    public class UsuarioAplicacion:IdentityUser
    {
        [Required(ErrorMessage = "El Campo Nombre es Requerido")]
        [MaxLength(80, ErrorMessage = "El Nombre solo se compone de 80 Caracteres como Máximo")]
        public string Nombres { get; set; }

        [MaxLength(80, ErrorMessage = "El Apellido solo se compone de 80 Caracteres como Máximo")]
        public string Apellidos { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
