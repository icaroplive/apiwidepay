using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models.Retorno;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private IWidePay _cob;
        private IFinanceiro _fin;
        public ValuesController (IWidePay cob, IFinanceiro fin) {
            _cob = cob;
            _fin = fin;
        }
        // GET api/values
        [HttpGet]
        public string[] Get () {
           var fin_movimento = _fin.boletosMais30Dias();
           _fin.atualizarBoletoMais30Dias(fin_movimento);
            // var result = cob.criarCobranca ();
           // var boleto = _cob.pegarCodigoBarra ("9A929CBF78A76544");
           // return boleto;
            return new string[] { "value1", "value2" };
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