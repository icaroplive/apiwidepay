using System;
using System.Collections.Generic;
using System.Linq;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Services {
    public class FinanceiroService : IFinanceiro {
        BancoContext _db;
        IWidePay _cob;

        IMysql _mysql;

        IBoletoStorage _boleto;
        public FinanceiroService (BancoContext db, IWidePay cob, IMysql mysql, IBoletoStorage boleto) {
            _db = db;
            _cob = cob;
            _mysql = mysql;
            _boleto = boleto;

        }
        public List<fin_movimento> boletosMais30Dias () {
            return _db.fin_movimento.Where (c => c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && DateTime.Now > c.data_boleto && EF.Functions.DateDiffDay (c.data_boleto, DateTime.Now) > 30).ToList (); //.Where (c => c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && DateTime.Now > c.data_boleto && DateTime.Now.Subtract (c.data_boleto).Days > 30).ToList ();
        }
        public void atualizarBoletoMais30Dias (List<fin_movimento> fin_movimento) {

            foreach (var x in fin_movimento) {
                x.data_boleto = DateTime.Now.AddDays (DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Sunday ? -9 : DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Saturday ? -8 : -10);

                _db.Entry (x).State = EntityState.Modified;

                _db.SaveChanges ();

            }
            foreach (var x in fin_movimento) {
                var boleto = _cob.criarCobranca (x.idfin_movimento);

                var codigo = _cob.pegarCodigoBarra (boleto.Result.id);

                x.idwidepay = boleto.Result.id;

                x.codigo_barra = codigo.Result.codigo;

                _db.Entry (x).State = EntityState.Modified;

                _db.SaveChanges ();
            }

        }

        public void atualizarBoletos (List<int> idfin_movimento) {
            var fin = _db.fin_movimento.Where (f => idfin_movimento.Contains (f.idfin_movimento)).ToList ();

            foreach (var x in fin) {
                x.data_boleto = DateTime.Now.AddDays (DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Sunday ? -9 : DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Saturday ? -8 : -10);

                _db.Entry (x).State = EntityState.Modified;

                _db.SaveChanges ();

            }
            foreach (var x in fin) {
                var boleto = _cob.criarCobranca (x.idfin_movimento);

                var codigo = _cob.pegarCodigoBarra (boleto.Result.id);

                x.idwidepay = boleto.Result.id;

                x.codigo_barra = codigo.Result.codigo;

                _db.Entry (x).State = EntityState.Modified;

                _db.SaveChanges ();
            }

        }

        public List<fin_movimento> cobrancasSemBoleto () {
            return _db.fin_movimento.Where (c => c.tipo_movimento == 2 && c.idwidepay == String.Empty && c.data_pag == DateTime.Parse ("01/01/0001 00:00:00")).ToList (); //.Where (c => c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && DateTime.Now > c.data_boleto && DateTime.Now.Subtract (c.data_boleto).Days > 30).ToList ();
        }

        public void gravarBoletoTxt (int idfin_movimento) {
            var fin_mov = _db.fin_movimento.Where (c => c.idfin_movimento == idfin_movimento && (c.idwidepay != String.Empty && c.idwidepay != null)).FirstOrDefault();
            if (fin_mov != null) {
                var boleto = _cob.pegarCodigoBarra (fin_mov.idwidepay).Result;
                _boleto.gravarTxt (idfin_movimento, boleto.html);
            }
        }

        public void gravarBoletosTxt () {
            var boletos = _db.fin_movimento.Where (c => c.tipo_movimento == 2 && (c.idwidepay != String.Empty && c.idwidepay != null) && c.data_pag == DateTime.Parse ("01/01/0001 00:00:00")).Select (c => c.idfin_movimento).ToList ();
            foreach (var x in boletos) {
                var fin_movimento = _mysql.buscarPorIdFinMovimento (x);
                var boleto = _cob.pegarCodigoBarra (fin_movimento.idwidepay).Result;
                _boleto.gravarTxt (x, boleto.html);
            }
        }
    }
}