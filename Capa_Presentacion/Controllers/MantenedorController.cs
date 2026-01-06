using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Mvc;

namespace Capa_Presentacion.Controllers
{
    public class MantenedorController : Controller
    {
        #region Categoria
        public IActionResult Categoria()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { listData = oLista });
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idCategoria == 0) resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            else resultado = new CN_Categoria().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;
            resultado = new CN_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        #endregion

        #region Marca
        public ActionResult Marca()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new();
            oLista = new CN_Marcas().Listar();
            return Json(new { listOfMarca = oLista });
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje;

            if (objeto.idMarca == 0) resultado = new CN_Marcas().Registrar(objeto, out mensaje);
            else resultado = new CN_Marcas().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool resultado = false;
            string mensaje;
            resultado = new CN_Marcas().Eliminar(id, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        #endregion

        #region Producto
        public ActionResult Producto()
        {
            return View();
        }
        #endregion
    }
}
