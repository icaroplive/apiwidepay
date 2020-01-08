using api_widepay.Models.Contas;
using api_widepay.Models.WidePay;

namespace api_widepay.Interfaces
{
    public interface IMysql
    {
         fin_movimento buscarPorIdFinMovimento(int idfin_movimento);
         void baixarPagamento (Notificacao not);

    }
}