﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;
using System.Collections.Specialized;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public BodegaController(IUnidadTrabajo unidadTrabajo)
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
            Bodega bodega = new Bodega();
            if(id == null)
            {
                //creamos un nuevo registro
                bodega.Estado = true;
                return View(bodega);
            }
            bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if(bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if(bodega.Id == 0)
                {
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "La Bodega se Creo con Exito";
                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "La Bodega se Actualizo con Exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar la Bodega";
            return View(bodega);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var bodegaDB = await _unidadTrabajo.Bodega.Obtener(id);
            if(bodegaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar el Registro en la Base de Datos" });
            }
            _unidadTrabajo.Bodega.Remover(bodegaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true,message = "Bodega Eliminada con Exito" });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new {data = todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Bodega.ObtenerTodos();

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
