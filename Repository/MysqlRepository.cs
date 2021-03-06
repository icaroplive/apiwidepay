using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using api_widepay.Models.WidePay;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Repository {
    public class MysqlRepository : IMysql {
        private BancoContext _banco;
        public MysqlRepository (BancoContext banco) {
            _banco = banco;
        }
        public fin_movimento buscarPorIdFinMovimento (int idfin_movimento) {
            var dados = (from b in _banco.fin_movimento where b.idfin_movimento == idfin_movimento select b).FirstOrDefault ();
            return dados;
        }
        public cad_cliente buscarClientePorId (int idcad_cliente) {
            var dados = (from b in _banco.cad_cliente where b.idcad_cliente == idcad_cliente select b).FirstOrDefault ();
            return dados;
        }
        public void atualizarFinMovimento (int idfin_movimento, string idwidepay, string codigo_barra) {
            var mov = (from f in _banco.fin_movimento where f.idfin_movimento == idfin_movimento select f).FirstOrDefault ();
            mov.codigo_barra = codigo_barra;
            mov.idwidepay = idwidepay;
            _banco.Entry (mov).State = EntityState.Modified;
            _banco.SaveChanges ();
            //await _banco.SaveChangesAsync ();
        }

        public fin_movimento baixarPagamento (Notificacao not) {

            fin_movimento fin = (from b in _banco.fin_movimento where b.idwidepay == not.cobranca.id select b).AsNoTracking ().FirstOrDefault ();
            if (fin != null) {
                fin.data_pag = not.cobranca.recebimento;
                fin.vlr_pag = not.cobranca.recebido - not.cobranca.tarifa; // fin.vlr_cob - fin.vlr_tarifa;
                fin.status_pagamento = 1;
                fin.idcad_conta = 3;
                _banco.Entry (fin).State = EntityState.Modified;
                _banco.SaveChanges ();
                return fin;
            } else return new fin_movimento ();
        }

    }
}