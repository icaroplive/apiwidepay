using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using api_widepay.Interfaces;
using api_widepay.Models;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;
using Newtonsoft.Json;

namespace api_widepay.Services {

    public class widepayService : IWidePay {
        private HttpClient _httpClient = new HttpClient ();
        private IMysql _mysql;
        private byte[] authToken = Encoding.ASCII.GetBytes ("175728:b3f8094795bcd4dd5c2e016e6230799a");
        public widepayService (IMysql mysql) {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", Convert.ToBase64String (authToken));
            _mysql = mysql;
        }
        public async Task<RetornoBoleto> criarCobranca (int idfin_movimento) {
            var mov = _mysql.buscarPorIdFinMovimento (idfin_movimento);
            var cli = _mysql.buscarClientePorId (mov.idcad_descricao);

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/adicionar";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("forma", "Boleto"));
            nvc.Add (new KeyValuePair<string, string> ("cliente", cli.nome));
            nvc.Add (new KeyValuePair<string, string> ("pessoa", "Física"));
            nvc.Add (new KeyValuePair<string, string> ("cpf", cli.cpfcnpj));
            nvc.Add (new KeyValuePair<string, string> ("itens[0][descricao]", mov.obs));
            nvc.Add (new KeyValuePair<string, string> ("itens[0][valor]", mov.vlr_cob.ToString ()));
            nvc.Add (new KeyValuePair<string, string> ("vencimento", String.Format ("yyyy-MM-dd", mov.data_venc)));
            nvc.Add (new KeyValuePair<string, string> ("boleto[juros]", "2"));
            nvc.Add (new KeyValuePair<string, string> ("boleto[multa]", "10"));
            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);
            return JsonConvert.DeserializeObject<RetornoBoleto> (result.Content.ReadAsStringAsync ().Result);;
        }
        public async Task<RetornoDadosBoleto> pegarCodigoBarra (string id) {

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/boleto";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("id", id));
            nvc.Add (new KeyValuePair<string, string> ("atualizar", "Sim"));
            nvc.Add (new KeyValuePair<string, string> ("html", "Sim"));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);

            return JsonConvert.DeserializeObject<RetornoDadosBoleto> (result.Content.ReadAsStringAsync ().Result);;
        }

        public async Task<Notificacao> consultarNotificacao (string id) {

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/notificacao";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("id", id));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);

            return JsonConvert.DeserializeObject<Notificacao> (result.Content.ReadAsStringAsync ().Result);;
        }

        public async Task<RetornoCobrancas> consultarCobranca (string id) {

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/consultar";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("id", id));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);

            return JsonConvert.DeserializeObject<RetornoCobrancas> (result.Content.ReadAsStringAsync ().Result);;
        }

        public async Task<bool> receberManual (string id) {

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/manual";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("id", id));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);

            return true;
        }
    }
}