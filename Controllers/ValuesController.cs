using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models.Retorno;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private IWidePay _cob;
        public ValuesController (IWidePay cob) {
            _cob = cob;
        }
        // GET api/values
        [HttpGet]
        public Task<RetornoDadosBoleto> Get () {
            // var result = cob.criarCobranca ();
            var boleto = _cob.pegarCodigoBarra ("9A929CBF78A76544");
            return boleto;
            //return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}