using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models.WidePay;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        public NotificacaoController (IMysql mysql, IWidePay cob) {
            _mysql = mysql;
            _cob = cob;
        }

        [HttpGet ("{id}")]
        public Task<Notificacao> Get (string id) {
            var not = _cob.consultarNotificacao (id);
            if (not.Result.cobranca.recebimento != null) {
                _mysql.baixarPagamento (not.Result);
            }
            return not;
        }

    }
}