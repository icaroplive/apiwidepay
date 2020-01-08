using api_widepay.Models.Contas;
using api_widepay.Models.WidePay;

namespace api_widepay.Interfaces
{
    public interface IMysql
    {
         fin_movimento buscarPorIdFinMovimento(int idfin_movimento);
         cad_cliente buscarClientePorId(int idcad_cliente);
         void atualizarFinMovimento (int idfin_movimento, string idwidepay, string codigo_barra);
         fin_movimento baixarPagamento (Notificacao not);

    }
}