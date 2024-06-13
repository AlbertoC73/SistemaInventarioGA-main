using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IUsuarioiAplicacionRepositorio UsuarioAplicacion {  get; }
        IInventarioRepositorio Inventario { get; }
        ITratamientoRepositorio Tratamiento { get; }
        ICitaRepositorio Cita { get; }

        Task Guardar();
    }
}
