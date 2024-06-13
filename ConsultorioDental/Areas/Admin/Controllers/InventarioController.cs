using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using ConsultorioDental.Utilidades;
using System.Collections.Specialized;

namespace ConsultorioDental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Dentista)]
    public class InventarioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public InventarioController(IUnidadTrabajo unidadTrabajo)
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
            Modelos.Inventario inventario = new Modelos.Inventario();
            if(id == null)
            {
                //creamos un nuevo registro
                inventario.Estado = true;
                return View(inventario);
            }
            inventario = await _unidadTrabajo.Inventario.Obtener(id.GetValueOrDefault());
            if(inventario == null)
            {
                return NotFound();
            }
            return View(inventario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Modelos.Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                if(inventario.Id == 0)
                {
                    await _unidadTrabajo.Inventario.Agregar(inventario);
                    TempData[DS.Exitosa] = "La Inventario se Creo con Exito";
                }
                else
                {
                    _unidadTrabajo.Inventario.Actualizar(inventario);
                    TempData[DS.Exitosa] = "La Inventario se Actualizo con Exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar la Inventario";
            return View(inventario);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var inventarioDB = await _unidadTrabajo.Inventario.Obtener(id);
            if(inventarioDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar el Registro en la Base de Datos" });
            }
            _unidadTrabajo.Inventario.Remover(inventarioDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true,message = "Inventario Eliminada con Exito" });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Inventario.ObtenerTodos();
            return Json(new {data = todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Inventario.ObtenerTodos();

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
