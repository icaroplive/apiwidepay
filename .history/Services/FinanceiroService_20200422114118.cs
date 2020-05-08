using api_widepay.Entities;
using api_widepay.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace api_widepay.Services
{
    public class FinanceiroService : IFinanceiro
    {
        BancoContext _db;
        public FinanceiroService(BancoContext db){
            _db = db;

        }
        public void boletosMais30Dias()
        {
            var x = _db.fin_movimento.Where(c => c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && DateTime.Now > c.data_venc && DateTime.Now.Subtract(c.data_venc).Days > 30).ToList();
           
            throw new System.NotImplementedException();
        }
    }
}