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
        private IBoletoStorage _boletoStorage;
        public CobrancaController (IMysql mysql, IWidePay cob, IBoletoStorage boletoStorage) {
            _mysql = mysql;
            _cob = cob;
            _boletoStorage = boletoStorage;
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
        public List<RetornoDadosBoleto> Post ([FromBody] List<int> idfin_movimento) {
            var lista = new List<RetornoDadosBoleto> ();
            foreach (var x in idfin_movimento) {
                var result = _cob.criarCobranca (x);
                var boleto = _cob.pegarCodigoBarra (result.Result.id).Result;
                _mysql.atualizarFinMovimento (x, result.Result.id, boleto.codigo);
                boleto.idfin_movimento = x;
                _boletoStorage.gravarTxt (x, boleto.html);
                lista.Add (boleto);
            }
            return lista;
        }

        [Route ("api/codigoBarra/{id}")]
        [HttpGet ("{id}")]
        public Task<RetornoDadosBoleto> codigoBarra (int id) {
            var fin_movimento = _mysql.buscarPorIdFinMovimento (id);
            var boleto = _cob.pegarCodigoBarra (fin_movimento.idwidepay);
            _boletoStorage.gravarTxt (fin_movimento.idfin_movimento, boleto.Result.html);
            _mysql.atualizarFinMovimento (id, fin_movimento.idwidepay, boleto.Result.codigo);
            return boleto;
        }

        [Route ("api/syncBoleto")]
        [HttpPut]
        public List<RetornoDadosBoleto> Put ([FromBody] List<int> idfin_movimento) {
            var lista = new List<RetornoDadosBoleto> ();
            foreach (var x in idfin_movimento) {
                var fin_movimento = _mysql.buscarPorIdFinMovimento (x);
                var boleto = _cob.pegarCodigoBarra (fin_movimento.idwidepay).Result;
                _boletoStorage.gravarTxt (x, boleto.html);
                lista.Add (boleto);
            }
            return lista;
        }
    }
}