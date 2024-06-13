using ConsultorioDental.AccesoDatos.Repositorio;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.AccesoDatos.Data;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IUsuarioiAplicacionRepositorio UsuarioAplicacion { get; set; }
        public IInventarioRepositorio Inventario { get; set; }
        public ITratamientoRepositorio Tratamiento { get; set; }
        public ICitaRepositorio Cita { get; set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            Inventario = new InventarioRepositorio(_db);
            Tratamiento = new TratamientoRepositorio(_db);
            Cita = new CitaRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
