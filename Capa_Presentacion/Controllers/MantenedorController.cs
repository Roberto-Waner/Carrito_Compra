using Capa_Entidad;
using Capa_Negocio;
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

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista });
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idCategoria == 0) resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            else resultado = new CN_Categoria().Editar(objeto, out mensaje);

            return Json(new {resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;
            resultado = new CN_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje });
        }
    }
}
