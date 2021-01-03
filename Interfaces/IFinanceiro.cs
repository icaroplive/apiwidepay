using System.Collections.Generic;
using api_widepay.Models.Contas;

namespace api_widepay.Interfaces {
    public interface IFinanceiro {
        List<fin_movimento> boletosMais30Dias ();
        List<fin_movimento> cobrancasSemBoleto ();
        void atualizarBoletoMais30Dias (List<fin_movimento> fin_movimento);
        void atualizarBoletos (List<int> idfin_movimento);
        void gravarBoletoTxt (int idfin_movimento);
        void gravarBoletosTxt ();
    }
}