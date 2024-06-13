using ConsultorioDental.AccesoDatos.Data;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Repositorio
{
    public class InventarioRepositorio : Repositorio<Inventario>, IInventarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public InventarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Inventario inventario)
        {
            var inventarioBD = _db.Inventarios.FirstOrDefault(b => b.Id == inventario.Id);
            if (inventarioBD != null)
            {
                inventarioBD.Nombre = inventario.Nombre;
                inventarioBD.Descripcion = inventario.Descripcion;
                inventarioBD.Cantidad = inventario.Cantidad;
                inventarioBD.Estado = inventario.Estado;
                _db.SaveChanges();
            }
        }
    }
}
