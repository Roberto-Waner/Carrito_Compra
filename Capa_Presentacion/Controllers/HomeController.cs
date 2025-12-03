using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Mvc;

namespace Capa_Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();

            //return Json(oLista, JsonRequestBehavior.AllowGet); // Versiones anteriores a .NET Core
            //return Json(oLista);
            return Json(new { data = oLista });
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object respuesta;
            string mensaje = string.Empty;

            if (objeto.idUsuario == 0) respuesta = new CN_Usuarios().Registrar(objeto, out mensaje);
            else respuesta = new CN_Usuarios().Editar(objeto, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje });
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        //public ActionResult PaginaTest()
        //{
        //    return View();
        //}
    }
}
