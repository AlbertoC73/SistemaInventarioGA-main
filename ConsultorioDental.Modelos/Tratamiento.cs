using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.Modelos
{
    public class Tratamiento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El Campo Nombre es Requerido")]
        [MaxLength(60,ErrorMessage ="El Nombre solo se compone de 60 Caracteres como Máximo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo Descripción es Requerido")]
        [MaxLength(100, ErrorMessage = "La Descripción solo se compone de 100 Caracteres como Máximo")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Campo Costo es Requerido")]
        [MaxLength(7, ErrorMessage = "El Costo solo se compone de 7 Caracteres como Máximo")]
        public string Costo { get; set; }
        [Required(ErrorMessage = "El Estado de el Tratamiento es Requerido")]
        public bool Estado { get; set; }
    }
}
