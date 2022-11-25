using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraASPDesafio.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Calcular(string Visor)
        {
            
            var servico = new Servico.ServicoCalculadora();

            var resultado = servico.CalcularTudo(Visor);

            return Json(new {data = resultado }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}