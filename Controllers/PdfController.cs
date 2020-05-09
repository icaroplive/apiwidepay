using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models.Retorno;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase {

        private IWidePay _wide;
        public PdfController (IWidePay wide) {
            _wide = wide;
        }

        [HttpGet ("{filename}")]
        public IActionResult Get (string filename) {
            const string contentType = "application/pdf";
            HttpContext.Response.ContentType = contentType;
            var result = new FileContentResult (System.IO.File.ReadAllBytes (@"boletos//" + filename), contentType) {
                FileDownloadName = $"{filename}"
            };

            return result;
        }

        [HttpPost]
        public string Post ([FromBody] List<int> idfin_movimento) {
            var file = _wide.geraPDF (idfin_movimento).Result;
            return file;
        }
    }
}