using ConsultorioDental.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICitaRepositorio : IRepositorio<Cita>
    {
        void Actualizar(Cita cita);
        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);
    }
}
