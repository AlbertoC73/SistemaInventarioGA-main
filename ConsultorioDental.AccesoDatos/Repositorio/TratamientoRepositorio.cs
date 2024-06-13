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
    public class TratamientoRepositorio : Repositorio<Tratamiento>, ITratamientoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public TratamientoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Tratamiento tratamiento)
        {
            var tratamientoBD = _db.Tratamientos.FirstOrDefault(b => b.Id == tratamiento.Id);
            if (tratamientoBD != null)
            {
                tratamientoBD.Nombre = tratamiento.Nombre;
                tratamientoBD.Descripcion = tratamiento.Descripcion;
                tratamientoBD.Costo = tratamiento.Costo;
                tratamientoBD.Estado = tratamiento.Estado;
                _db.SaveChanges();
            }
        }
    }
}
