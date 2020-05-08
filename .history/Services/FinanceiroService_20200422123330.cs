using api_widepay.Entities;
using api_widepay.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using api_widepay.Models.Contas;

namespace api_widepay.Services
{
    public class FinanceiroService : IFinanceiro
    {
        BancoContext _db;
        public FinanceiroService(BancoContext db){
            _db = db;

        }
        public List<fin_movimento> boletosMais30Dias()
        {
            return _db.fin_movimento.Where(c => c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && DateTime.Now > c.data_boleto && DateTime.Now.Subtract(c.data_venc).Days > 30).ToList();
        }
        public void atualizarBoletoMais30Dias(List<fin_movimento> fin_movimento) {

            foreach (var x in fin_movimento) {
                x.data_boleto = DateTime.Now.AddDays(DateTime.Now.AddDays(-10).DayOfWeek == DayOfWeek.Sunday ? -9 : DateTime.Now.AddDays(-10).DayOfWeek == DayOfWeek.Saturday ? -8 : -10 );
            }
            

        }
    }
}