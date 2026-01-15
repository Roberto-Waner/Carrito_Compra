using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Globalization;

namespace Capa_Presentacion.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public MantenedorController(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

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
            //Cargar las Marcas
            //var marcas = new CN_Marcas().Listar();
            //var listBrands = marcas.Select(m => new { Value = m.idMarca.ToString(), Text = m.description }).ToList();

            //ViewBag.ListarMarcas = listBrands;
            ViewBag.ListarMarcas = new CN_Marcas()
                .Listar()
                .ToDictionary(
                    m => m.idMarca.ToString(),
                    m => m.description
                );

            //Cargar las Categorias
            //var categorias = new CN_Categoria().Listar();
            //var listCtgries = categorias.Select(c => new { Value = c.idCategoria.ToString(), Text = c.description }).ToList();

            //ViewBag.ListarCategorias = listCtgries;
            ViewBag.ListarCategorias = new CN_Categoria()
                .Listar()
                .ToDictionary(
                    c => c.idCategoria.ToString(),
                    c => c.description
                );

            return View();
        }

        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new();
            oLista = new CN_Productos().Listar();
            return Json(new { listOfProd = oLista });
        }

        [HttpPost]
        public async Task<JsonResult> GuardarProducto(string objeto, IFormFile imgFile)
        {
            string mensaje;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto)!;

            // Validación del precio
            if (decimal.TryParse(oProducto.PrecioText, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out decimal precio)) oProducto.precio = precio;
            else return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe de ser ##.##" });

            // Registrar o editar producto
            if (oProducto.idProducto == 0)
            {
                int idProductoGenerado = new CN_Productos().Registrar(oProducto, out mensaje);

                if (idProductoGenerado != 0) 
                    oProducto.idProducto = idProductoGenerado;
                else 
                    operacion_exitosa = false;
            }
            else operacion_exitosa = new CN_Productos().Editar(oProducto, out mensaje);

            if (operacion_exitosa) // Guardar imagen si todo salió bien y hay archivo
            {
                if(imgFile != null && imgFile.Length > 0)
                {
                    // Leer la ruta relativa desde appsettings.json
                    string relativeRoute = _config["FileContainer:RutaImagenes"]!;

                    // Combinar con ContentRootPath para obtener ruta física absoluta
                    string ruta_guardado = Path.Combine(_env.ContentRootPath, relativeRoute.TrimStart('.', '\\', '/'));

                    if(!Directory.Exists(ruta_guardado)) Directory.CreateDirectory(ruta_guardado);

                    string extension = Path.GetExtension(imgFile.FileName);
                    string nombre_imagen = $"{oProducto.idProducto}{extension}";
                    string imgRoute = Path.Combine(ruta_guardado, nombre_imagen);

                    try
                    {
                        //imgFile.SaveAs(Path.Combine(ruta_guardado, nombre_imagen));
                        using var stream = new FileStream(imgRoute, FileMode.Create);
                        await imgFile.CopyToAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.rutaImagen = nombre_imagen;
                        oProducto.nombreImagen = nombre_imagen;
                        bool rspta = new CN_Productos().GuardarImagen(oProducto, out mensaje);
                        if (!rspta)
                            mensaje = "Producto guardado pero fallo al actualizar la ruta de imagen en BD";
                    }
                    else mensaje = "Se guardaron el producto pero hubo problemas con la imagen";
                }
            }

            return Json(new 
            { 
                operacionExitosa = operacion_exitosa, 
                idGenerado = oProducto.idProducto, 
                mensaje = mensaje 
            });
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            Producto oProducto = new CN_Productos().Listar().Where(p => p.idProducto == id).FirstOrDefault()!;

            string textBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaImagen, oProducto.nombreImagen), out bool conversion);

            return Json(new
            {
                conversion = conversion,
                textBase64 = textBase64,
                extension = Path.GetExtension(oProducto.nombreImagen)
            });
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool resultado = false;
            string mensaje;
            resultado = new CN_Productos().Eliminar(id, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje });
        }
        #endregion
    }
}
