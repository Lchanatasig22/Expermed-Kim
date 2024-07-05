using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Expermed.Models;

namespace Expermed.Controllers
{
    public class PdfController : Controller
    {
        public IActionResult GeneratePdf(bool receta, bool justificacion, bool formatoConsulta)
        {
            var model = new PdfViewModel
            {
                //Receta = receta,
                //Justificacion = justificacion,
                //FormatoConsulta = formatoConsulta
            };

            return new ViewAsPdf("GeneratePdf", model)
            {
                FileName = "Reporte.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }


       
    }
}
