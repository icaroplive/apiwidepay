using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    // [Route ("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        private BancoContext _banco;
        public NotificacaoController (IMysql mysql, IWidePay cob, BancoContext banco) {
            _mysql = mysql;
            _cob = cob;
            _banco = banco;
        }

        [Route ("api/notificacao")]
        [HttpGet]
        public void notificacao (string id) {
            _cob.atualizarValorRecebido ();
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
        public Task<bool> PagarManual (string id) {
            var not = _cob.receberManual (id);
            return not;
        }

        [Route ("api/consultarcobranca/{id}")]
        public Task<RetornoCobrancas> consultarcobranca (string id) {
            var not = _cob.consultarCobranca (id);
            return not;
        }

        [Route ("api/consultarcobrancas")]
        public Cobrancas consultarcobrancas () {
            var cob = _cob.consultarCobrancas ().Result;
            foreach (var x in cob.cobrancas) {
                var update = _banco.fin_movimento.Where (f => f.idwidepay == x.id).FirstOrDefault ();
                if (update != null) {
                    update.vlr_pag = x.recebido;
                    update.vlr_tarifa = x.tarifa;
                    update.data_pag = x.recebimento;

                    _banco.Update (update);
                    _banco.SaveChanges ();
                }
            }
            return _cob.consultarCobrancas ().Result;
        }

        [HttpPost]
        [Route ("api/notificacao")]
        public fin_movimento Post ([FromForm] PayLoadNotificacao payload) {
            fin_movimento fin = new fin_movimento ();
            var not = _cob.consultarNotificacao (payload.notificacao).Result;
            if (not.cobranca != null && not.sucesso == true && not.cobranca.status == "Recebido") {
                fin = _mysql.baixarPagamento (not);
            }

             return fin;

        }

    }
}