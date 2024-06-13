using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.Modelos.ViewModels
{
    public class CitaVM
    {
        public Cita Cita { get; set; }
        public IEnumerable<SelectListItem> TratamientoLista { get; set; }
        public IEnumerable<SelectListItem> UsuarioAplicacionLista { get; set; }
    }
}
