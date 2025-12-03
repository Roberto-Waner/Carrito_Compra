using Microsoft.AspNetCore.Mvc;

namespace Capa_Presentacion.Controllers
{
    public class MantenedorController : Controller
    {
        public IActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }
    }
}
