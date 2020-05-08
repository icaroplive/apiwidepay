using api_widepay.Entities;
using api_widepay.Interfaces;

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
            throw new System.NotImplementedException();
        }
    }
}