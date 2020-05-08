using System.Collections.Generic;
using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace api_widepay.Controllers {
    // [Route ("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        public NotificacaoController (IMysql mysql, IWidePay cob) {
            _mysql = mysql;
            _cob = cob;
        }

        [Route ("api/notificacao/{id}")]
        //[HttpGet ("{id}")]
        public Task<Notificacao> Get (string id) {
            var not = _cob.consultarNotificacao (id);
            if (not.Result.cobranca.recebimento != null) {
                //     _mysql.baixarPagamento (not.Result);
            }
            return not;
        }

        [Route ("api/recebermanual/{id}")]
        //[HttpGet ("{id}")]
        public Task<bool> PagarManual (string id) {
            var not = _cob.receberManual (id);
            return not;
        }

        [Route ("api/consultarcobranca/{id}")]
        //[HttpGet ("{id}")]
        public Task<RetornoCobrancas> consultarcobranca (string id) {
            var not = _cob.consultarCobranca (id);
            return not;
        }

        [Route ("api/consultarcobrancas")]
        public Task<Cobrancas> consultarcobrancas () {
            var not = _cob.consultarCobrancas ();
            return not.Result.cobrancas.;
        }

        [HttpPost]
        [Route ("api/notificacao")]
        public fin_movimento Post ([FromForm] PayLoadNotificacao payload) {
            fin_movimento fin = new fin_movimento ();
            var not = _cob.consultarNotificacao (payload.notificacao);
            if (not.Result.cobranca.status == "Recebido") {
                fin = _mysql.baixarPagamento (not.Result);
            }
            return fin;

        }
        /*[Route ("api/addcobranca/{id}")]
        //[HttpGet ("{id}")]
        public fin_movimento addcobranca (int id) {
            var not = _mysql.buscarPorIdFinMovimento (id);
            return not;
        }*/

    }
}