using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;
using api_widepay.Models.WidePay;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_widepay.Services {

    public class widepayService : IWidePay {
        private HttpClient _httpClient = new HttpClient ();
        private IMysql _mysql;

        private BancoContext _db;

        private readonly IHostingEnvironment _hostingEnvironment;
        private byte[] authToken = Encoding.ASCII.GetBytes ("75501:452f1d9e5b5b285867f486837f658305");
        public widepayService (IMysql mysql, BancoContext db, IHostingEnvironment hostingEnvironment) {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", Convert.ToBase64String (authToken));
            _mysql = mysql;
        }
        public async Task<RetornoBoleto> criarCobranca (int idfin_movimento) {
            var mov = _mysql.buscarPorIdFinMovimento (idfin_movimento);

            if (DateTime.Now  > mov.data_boleto) {
                mov.data_boleto = DateTime.Now.AddDays (DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Sunday ? -9 : DateTime.Now.AddDays (-10).DayOfWeek == DayOfWeek.Saturday ? -8 : -10);
            }
            var cli = _mysql.buscarClientePorId (mov.idcad_descricao);

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/adicionar";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("forma", "Boleto"));
            nvc.Add (new KeyValuePair<string, string> ("cliente", cli.nome));
            nvc.Add (new KeyValuePair<string, string> ("pessoa", "F??sica"));
            nvc.Add (new KeyValuePair<string, string> ("cpf", cli.cpfcnpj));
            nvc.Add (new KeyValuePair<string, string> ("itens[0][descricao]", mov.obs));
            nvc.Add (new KeyValuePair<string, string> ("itens[0][valor]", mov.vlr_cob.ToString ()));
            nvc.Add (new KeyValuePair<string, string> ("vencimento", Convert.ToDateTime (mov.data_boleto).ToString ("yyyy-MM-dd")));
            nvc.Add (new KeyValuePair<string, string> ("notificacao", "http://pagamento.ipr.net.br/api/notificacao"));
            nvc.Add (new KeyValuePair<string, string> ("referencia", idfin_movimento.ToString ()));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);
            return JsonConvert.DeserializeObject<RetornoBoleto> (result.Content.ReadAsStringAsync ().Result);
        }
        public async Task<RetornoDadosBoleto> pegarCodigoBarra (string id) {

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/boleto";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("id", id));
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

            return JsonConvert.DeserializeObject<RetornoCobrancas> (result.Content.ReadAsStringAsync ().Result);
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

        public async Task<Cobrancas> consultarCobrancas () {
            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/consultar";

            var nvc = new List<KeyValuePair<string, string>> ();

            nvc.Add (new KeyValuePair<string, string> ("filtro[0][]", "forma"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[0][]", "[]"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[0][2][]", "Boleto"));

            nvc.Add (new KeyValuePair<string, string> ("filtro[1][]", "recebimento"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[1][]", ">="));
            nvc.Add (new KeyValuePair<string, string> ("filtro[1][]", "2020-04-01 00:00:00"));

            nvc.Add (new KeyValuePair<string, string> ("filtro[2][]", "recebimento"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[2][]", "<="));
            nvc.Add (new KeyValuePair<string, string> ("filtro[2][]", "2020-07-09 23:59:59"));

            nvc.Add (new KeyValuePair<string, string> ("filtro[3][]", "status"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[3][]", "[]"));
            nvc.Add (new KeyValuePair<string, string> ("filtro[3][2][]", "Recebido"));
            nvc.Add (new KeyValuePair<string, string> ("limite", "1000"));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };
            var result = await _httpClient.SendAsync (request);

            return JsonConvert.DeserializeObject<Cobrancas> (result.Content.ReadAsStringAsync ().Result);
        }

        public async Task<RetornoCancela> cancelarCobrancas (List<string> idwidepay) {

            //   var content = String.Format ("{{'id' : [{0}] }}", String.Join ("'", idwidepay.ToArray ()));
            //var content = JsonConvert.SerializeObject (new CancelaModel () { id = idwidepay });

            var url = "https://api.widepay.com/v1/recebimentos/cobrancas/cancelar";

            var nvc = new List<KeyValuePair<string, string>> ();

            nvc.Add (new KeyValuePair<string, string> ("id", String.Format ("[\"{0}\"]", String.Join ("\",\"", idwidepay.ToArray ()))));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };

            var result = await _httpClient.SendAsync (request);

            return JsonConvert.DeserializeObject<RetornoCancela> (result.Content.ReadAsStringAsync ().Result);
        }

        public async Task<string> geraPDF (List<int> idfin_movimento) {

            //   var content = String.Format ("{{'id' : [{0}] }}", String.Join ("'", idwidepay.ToArray ()));
            //var content = JsonConvert.SerializeObject (new CancelaModel () { id = idwidepay });

            var url = "https://www.widepay.com/api/recebimentos/pdf/gerar";

            var nvc = new List<KeyValuePair<string, string>> ();
            nvc.Add (new KeyValuePair<string, string> ("capa", "N??o"));
            nvc.Add (new KeyValuePair<string, string> ("tipo", "Boleto"));

            var idwidepay = _db.fin_movimento.Where (ff => idfin_movimento.Contains (ff.idfin_movimento)).Select (f => f.idwidepay).ToList ();
            foreach (var x in idwidepay) {
                nvc.Add (new KeyValuePair<string, string> ("registros[]", x));
            }
            //nvc.Add (new KeyValuePair<string, string> ("id", String.Format ("[\"{0}\"]", String.Join ("\",\"", idwidepay.ToArray ()))));

            var request = new HttpRequestMessage {
                RequestUri = new Uri (url),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent (nvc)
            };

            var result = await _httpClient.SendAsync (request);

            var xxx = JsonConvert.DeserializeObject<RetornoGerarPDF> (result.Content.ReadAsStringAsync ().Result);

            return this.downloadPDF (xxx.id).Result;

            //return ;
        }

        public async Task<string> downloadPDF (int id) {

            var url = "https://www.widepay.com/api/recebimentos/pdf/download?id=" + id;

            var response = await _httpClient.GetAsync (url);
            var path = string.Format ("{1}/boletos/{0}.pdf", id, _hostingEnvironment.ContentRootPath);
            var fs = new FileStream (path, File.Exists (path) ? FileMode.Open : FileMode.CreateNew);
            await response.Content.CopyToAsync (fs);
            fs.Close ();
            return string.Format ("{0}.pdf", id);
            // return fs;
        }

        public void atualizarValorRecebido () {
            var cobrancas = _db.fin_movimento.Where (f => f.idwidepay != null && f.idwidepay != "" && f.status_pagamento == 1).AsNoTracking ().ToList ();

            foreach (var x in cobrancas) {
                var widepay = this.consultarCobranca (x.idwidepay).Result;
                fin_movimento fin = (from b in _db.fin_movimento where b.idfin_movimento == x.idfin_movimento select b).AsNoTracking ().FirstOrDefault ();
                fin.vlr_pag = widepay.cobrancas[0].recebido - widepay.cobrancas[0].tarifa;
                fin.vlr_tarifa = widepay.cobrancas[0].tarifa;
                _db.Entry (fin).State = EntityState.Modified;
                _db.SaveChanges ();
            }
        }

    }
}