using ConsultorioDental.AccesoDatos.Data;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using ConsultorioDental.Utilidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Repositorio
{
    public class CitaRepositorio : Repositorio<Cita>, ICitaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CitaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Cita cita)
        {
            var citaBD = _db.Citas.FirstOrDefault(b => b.Id == cita.Id);
            if (citaBD != null)
            {
                citaBD.Fecha = cita.Fecha;
                citaBD.Hora = cita.Hora;
                citaBD.TratamientoId = cita.TratamientoId;
                citaBD.UsuarioAplicacionId = cita.UsuarioAplicacionId;
                citaBD.Estado = cita.Estado;
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            if (obj == "Tratamiento")
            {
                return _db.Tratamientos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre + " - $ " + c.Costo,
                    Value = c.Id.ToString()
                });
            }
            if (obj == "UsuarioAplicacion")
            {
                return _db.UsuarioAplicacion
                        .Where(c => _db.UserRoles.Any(x => _db.Roles.Any(y => y.Name == DS.Role_Paciente && y.Id == x.RoleId) && x.UserId == c.Id))
                        .Select(c => new SelectListItem
                        {
                            Text = c.Nombres +"  "+ c.Apellidos,
                            Value = c.Id
                        });
            }
            return null;
        }


    }
}
