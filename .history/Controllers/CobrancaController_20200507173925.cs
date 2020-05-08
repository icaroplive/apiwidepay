using System.Collections.Generic;
using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models;
using api_widepay.Models.Retorno;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    public class CobrancaController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        public CobrancaController (IMysql mysql, IWidePay cob) {
            _mysql = mysql;
            _cob = cob;
        }

        [Route ("api/addCobranca/{id}")]
        [HttpGet ("{id}")]
        public Task<RetornoBoleto> Get (int id) {
            var result = _cob.criarCobranca (id);
            var boleto = _cob.pegarCodigoBarra (result.Result.id);
            _mysql.atualizarFinMovimento (id, result.Result.id, boleto.Result.codigo);
            return result;
        }

        [Route ("api/addCobranca")]
        [HttpPost]
        public Task<List<RetornoBoleto>> Post ([FromBody] List<int> idfin_movimento) {
            var lista = new List<RetornoBoleto>();
            foreach (x in idfin_movimento) {
                var result = _cob.criarCobranca (x);
                var boleto = _cob.pegarCodigoBarra (result.Result.id);
                _mysql.atualizarFinMovimento (id, result.Result.id, boleto.Result.codigo);
            }
            return result;
        }

        [Route ("api/codigoBarra/{id}")]
        [HttpGet ("{id}")]
        public Task<RetornoDadosBoleto> codigoBarra (int id) {
            var fin_movimento = _mysql.buscarPorIdFinMovimento (id);
            var boleto = _cob.pegarCodigoBarra (fin_movimento.idwidepay);
            _mysql.atualizarFinMovimento (id, fin_movimento.idwidepay, boleto.Result.codigo);
            return boleto;
        }
    }
}