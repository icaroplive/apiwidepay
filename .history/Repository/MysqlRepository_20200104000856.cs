using System.Linq;
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
            var dados = (from b in _banco.fin_movimento select b).FirstOrDefault ();
            return dados;
        }

        public async void baixarPagamento (Notificacao not) {
            fin_movimento fin = (from b in _banco.fin_movimento where b.idwidepay == not.cobranca.id select b).FirstOrDefault ();

            fin.data_pag = not.cobranca.recebimento;
            fin.vlr_pag = fin.vlr_cob - fin.vlr_tarifa;
            fin.status_pagamento = 1;
            fin.idcad_conta = 3;

            _banco.Entry (fin).State = EntityState.Modified;

            await _banco.SaveChangesAsync ();

        }
    }
}