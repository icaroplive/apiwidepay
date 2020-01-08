using System.Collections.Generic;
using api_widepay.Interfaces;
using api_widepay.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_widepay.Controllers {
    public class CobrancaController : ControllerBase {
        private IMysql _mysql;
        private IWidePay _cob;
        public CobrancaController (IMysql mysql, IWidePay cob) {
            _mysql = mysql;
            _cob = cob;
        }

        [Route ("api/notificacao/{id}")]
        //[HttpGet ("{id}")]
        public Task<void> Get (int idfin_movimento) {
            var mov = _mysql.buscarPorIdFinMovimento (idfin_movimento);
            var cli = _mysql.buscarClientePorId (mov.idcad_descricao);
            var cob = new Cobranca () {
                forma = "Boleto",
                pessoa = "Física",
                cliente = cli.nome,
                cpf = cli.cpfcnpj,
                vencimento = mov.data_venc,
                itens = new List<Item>() {
                    new Item() { valor = mov.vlr_cob , descricao = mov.obs, quantidade = 1 }
                }
                
                

            };
            if (not.Result.cobranca.recebimento != null) {
                //     _mysql.baixarPagamento (not.Result);
            }
            //return not;
        }
    }
}