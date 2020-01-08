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
        public Task<RetornoBoleto> Get (int idfin_movimento) {
            var result = _cob.criarCobranca (idfin_movimento);
            var boleto = _cob.pegarCodigoBarra(result.Result.id);
            _mysql.atualizarFinMovimento(idfin_movimento,result.Result.id,boleto.Result.codigo);
            return result;
        }
    }
}