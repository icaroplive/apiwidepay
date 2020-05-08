using System.Collections.Generic;
using api_widepay.Models.Contas;

namespace api_widepay.Interfaces
{
    public interface IFinanceiro
    {
        List<fin_movimento> boletosMais30Dias();
        void atualizarBoletoMais30Dias(List<fin_movimento> fin_movimento);
    }
}