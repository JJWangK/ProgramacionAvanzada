using PAP.Generators;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Pantalla de administración para generar datos de prueba.
    /// </summary>
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(GeneratorStatusService.GetStatuses());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generate(string tabla)
        {
            var generator = GeneratorCatalog.CreateAll().FirstOrDefault(g => g.TableName == tabla);
            if (generator == null)
            {
                TempData["Error"] = "La tabla seleccionada no existe.";
                return RedirectToAction("Index");
            }

            try
            {
                var count = generator.Generate();
                TempData["Mensaje"] = string.Format("Se generaron {0} registros en {1}.", count, generator.TableName);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo generar " + generator.TableName + ": " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
