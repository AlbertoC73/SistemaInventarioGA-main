//using ConsultorioDental.Modelos.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;

//namespace ConsultorioDental.Areas.Inventario.Controllers
//{
//    [Area("Inventario")]
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}



using Microsoft.AspNetCore.Mvc;
using ConsultorioDental.AccesoDatos.Repositorio.IRepositorio;
using ConsultorioDental.Modelos;
using ConsultorioDental.Modelos.Espesificaciones;
using ConsultorioDental.Modelos.ViewModels;
using System.Diagnostics;

namespace ConsultorioDental.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber = 1, string busqueda = "",
                                    string busquedaActual = "")
        {
            if (!String.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4
            };
            var resultado = _unidadTrabajo.Cita.ObtenerTodosPaginado(parametros);

            if (!String.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Cita.ObtenerTodosPaginado(parametros,
                    p => p.Fecha.Contains(busqueda));
            }

            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (pageNumber > 1)
            {
                ViewData["Previo"] = "";
            }
            if (resultado.MetaData.TotalPages <= pageNumber)
            {
                ViewData["Siguiente"] = "disabled";
            }

            return View(resultado);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
