using Newtonsoft.Json;
using RutasWeb3.Helpers;
using RutasWeb3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RutasWeb3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string pathFile)
        {
            if (string.IsNullOrEmpty(pathFile))
                return View();

            int[,] jsonMatrix;

            using (StreamReader r = new StreamReader(pathFile))
            {
                string json = r.ReadToEnd();
                jsonMatrix = JsonConvert.DeserializeObject<int[,]>(json);
            }

            FBAlgoritm fbAlgoritm = new FBAlgoritm(jsonMatrix);
            DAlgoritm dAlgoritm = new DAlgoritm(jsonMatrix);

            BestRoute fbBestRoute = fbAlgoritm.Work();
            BestRoute dBestRoute = dAlgoritm.Work();

            View viewModel = new View
            {
                dBestRoute = dBestRoute,
                fbBestRoute = fbBestRoute,
                jsonNodos = null
            };

            return View(viewModel);
            
        }

        [HttpPost]
        public ActionResult Index(View nodos)
        {
            if (nodos.jsonNodos.ContentLength > 0)
            {
                var fileName = Path.GetFileName(nodos.jsonNodos.FileName);
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);
                nodos.jsonNodos.SaveAs(path);

                return RedirectToAction("Index", new { pathFile = path });
            }

            return RedirectToAction("Index");
        }

    }
}
