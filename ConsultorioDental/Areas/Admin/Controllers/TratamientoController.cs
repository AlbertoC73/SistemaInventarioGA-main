using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using ConsultorioDental.Utilidades;
using System.Collections.Specialized;

namespace ConsultorioDental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Dentista + "," + DS.Role_Paciente)]
    public class TratamientoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public TratamientoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        //metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            Tratamiento tratamiento = new Tratamiento();
            if(id == null)
            {
                //creamos un nuevo registro
                tratamiento.Estado = true;
                return View(tratamiento);
            }
            tratamiento = await _unidadTrabajo.Tratamiento.Obtener(id.GetValueOrDefault());
            if(tratamiento == null)
            {
                return NotFound();
            }
            return View(tratamiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                if(tratamiento.Id == 0)
                {
                    await _unidadTrabajo.Tratamiento.Agregar(tratamiento);
                    TempData[DS.Exitosa] = "El Tratamiento se Creo con Exito";
                }
                else
                {
                    _unidadTrabajo.Tratamiento.Actualizar(tratamiento);
                    TempData[DS.Exitosa] = "El Tratamiento se Actualizo con Exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar el Tratamiento";
            return View(tratamiento);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var tratamientoDB = await _unidadTrabajo.Tratamiento.Obtener(id);
            if(tratamientoDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar el Registro en la Base de Datos" });
            }
            _unidadTrabajo.Tratamiento.Remover(tratamientoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true,message = "Tratamiento Eliminado con Exito" });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Tratamiento.ObtenerTodos();
            return Json(new {data = todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Tratamiento.ObtenerTodos();

            if(id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() 
                                    == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim()
                                    == nombre.ToLower().Trim()
                                    && b.Id != id);
            }
            if(valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }



        #endregion
    }
}
