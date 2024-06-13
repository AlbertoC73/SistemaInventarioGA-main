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
    public class Cita
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo Fecha es Requerido")]
        [MaxLength(60,ErrorMessage = "La Fecha solo se compone de 60 Caracteres como Máximo")]
        public string Fecha { get; set; }
        [Required(ErrorMessage = "El Campo Hora es Requerido")]
        [MaxLength(100, ErrorMessage = "La Hora solo se compone de 100 Caracteres como Máximo")]
        public string Hora { get; set; }


        // Llave foránea para la relación con la tabla tratamiento
        [Required(ErrorMessage = "El Tratamiento es requerido")]
        public int TratamientoId { get; set; }
        [ForeignKey("TratamientoId")]
        public Tratamiento Tratamiento { get; set; }

        // Llave foránea para la relación con el paciente
        [Required(ErrorMessage = "El Paciente es requerida")]
        public string UsuarioAplicacionId { get; set; }
        [ForeignKey("UsuarioAplicacionId")]
        public UsuarioAplicacion UsuarioAplicacion { get; set; }


        [Required(ErrorMessage = "El estado de la Cita es Requerido")]
        public bool Estado { get; set; }
    }


}
