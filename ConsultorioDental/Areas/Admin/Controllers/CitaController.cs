using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using ConsultorioDental.Utilidades;
using System.Collections.Specialized;
using ConsultorioDental.Modelos.ViewModels;

namespace ConsultorioDental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Dentista + "," + DS.Role_Paciente)]
    public class CitaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CitaController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        //metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            CitaVM citaVM = new CitaVM()
            {
                Cita = new Cita(),
                TratamientoLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("Tratamiento"),
                UsuarioAplicacionLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("UsuarioAplicacion")
            };

            if (id == null)
            {
                //Crear un Cita Nuevos
                return View(citaVM);
            }
            else
            {
                //Actualizar un Cita Existente
                citaVM.Cita = await _unidadTrabajo.Cita
                    .Obtener(id.GetValueOrDefault());
                if (citaVM.Cita == null)
                {
                    return NotFound();
                }
                return View(citaVM);
            }
        }


        public async Task<IActionResult> Detalle(int? id)
        {
            CitaVM citaVM = new CitaVM()
            {
                Cita = new Cita(),
                TratamientoLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("Tratamiento"),
                UsuarioAplicacionLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("UsuarioAplicacion")
            };

            //Actualizar un Cita Existente
            citaVM.Cita = await _unidadTrabajo.Cita.Obtener(id.GetValueOrDefault());
            if (citaVM.Cita == null)
            {
                return NotFound();
            }
            return View(citaVM);
        }


        //metodo Upsert GET
        public async Task<IActionResult> UpsertD(int? id)
        {
            CitaVM citaVM = new CitaVM()
            {
                Cita = new Cita(),
                TratamientoLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("Tratamiento"),
                UsuarioAplicacionLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("UsuarioAplicacion")
            };

            if (id == null)
            {
                //Crear un Cita Nuevos
                return View(citaVM);
            }
            else
            {
                //Actualizar un Cita Existente
                citaVM.Cita = await _unidadTrabajo.Cita
                    .Obtener(id.GetValueOrDefault());
                if (citaVM.Cita == null)
                {
                    return NotFound();
                }
                return View(citaVM);
            }
        }


        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CitaVM CitaVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRooPath = _webHostEnvironment.WebRootPath;
                if (CitaVM.Cita.Id == 0)
                {
                    //Crear un nuevo Cita
                    string upload = webRooPath;
                    await _unidadTrabajo.Cita.Agregar(CitaVM.Cita);
                }
                else
                {
                    //actiaulizar el Cita
                    var objCita = await _unidadTrabajo.Cita.ObtenerPrimero(p => p.Id == CitaVM.Cita.Id, isTracking: false);
                    _unidadTrabajo.Cita.Actualizar(CitaVM.Cita);
                }
                TempData[DS.Exitosa] = "Cita Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }//si el Model State es falso
            CitaVM.TratamientoLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("Tratamiento");
            CitaVM.UsuarioAplicacionLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("UsuarioAplicacion");
            return View(CitaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertD(CitaVM CitaVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRooPath = _webHostEnvironment.WebRootPath;
                if (CitaVM.Cita.Id == 0)
                {
                    //Crear un nuevo Cita
                    string upload = webRooPath;
                    await _unidadTrabajo.Cita.Agregar(CitaVM.Cita);
                }
                else
                {
                    //actiaulizar el Cita
                    var objCita = await _unidadTrabajo.Cita.ObtenerPrimero(p => p.Id == CitaVM.Cita.Id, isTracking: false);
                    _unidadTrabajo.Cita.Actualizar(CitaVM.Cita);
                }
                TempData[DS.Exitosa] = "Cita Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }//si el Model State es falso
            CitaVM.TratamientoLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("Tratamiento");
            CitaVM.UsuarioAplicacionLista = _unidadTrabajo.Cita.ObtenerTodosDropDownList("UsuarioAplicacion");
            return View(CitaVM);
        }



        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Cita.ObtenerTodos(incluirPropiedades: "Tratamiento,UsuarioAplicacion");
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var citaDB = await _unidadTrabajo.Cita.Obtener(id);
            if (citaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar el Registro en la Base de Datos" });
            }

            _unidadTrabajo.Cita.Remover(citaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Cita Eliminado con Exito" });



        }

        #endregion
    }
}
