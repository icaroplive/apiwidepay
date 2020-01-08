using System.Threading.Tasks;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;

namespace api_widepay.Interfaces {
    public interface IWidePay {
        Task<RetornoBoleto> criarCobranca ();
        Task<RetornoDadosBoleto> pegarCodigoBarra (string id);
        Task<Notificacao> consultarNotificacao (string id);
        Task<bool> receberManual (string id)

    }
}