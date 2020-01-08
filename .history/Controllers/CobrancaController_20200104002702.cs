using api_widepay.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    public class CobrancaController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        public CobrancaController (IMysql mysql, IWidePay cob) {
            _mysql = mysql;
            _cob = cob;
        }
    }
}