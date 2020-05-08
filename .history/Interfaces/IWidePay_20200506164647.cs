using System.Collections.Generic;
using System.Threading.Tasks;
using api_widepay.Models;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;

namespace api_widepay.Interfaces {
    public interface IWidePay {
        Task<RetornoBoleto> criarCobranca (int idfin_movimento);
        Task<RetornoDadosBoleto> pegarCodigoBarra (string id);
        Task<Notificacao> consultarNotificacao (string id);
        Task<RetornoCobrancas> consultarCobranca (string id);
        Task<Cobrancas> consultarCobrancas ();
        Task<bool> receberManual (string id);
        Task<RetornoCancela> cancelarCobrancas(CancelaModel cancelaLista);

    }
}