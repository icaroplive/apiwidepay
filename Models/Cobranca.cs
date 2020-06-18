using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api_widepay.Models {
    public class Cobranca {
        public Cobranca () {
            this.forma = "Boleto";
            this.notificacao = "https://ipr.net.br/segunda-via/widepay/retorno";
            this.pessoa = "Física";
        }
        public string id { get; set; }
        public string forma { get; set; }
        public string cliente { get; set; }
        public string pessoa { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public Endereco endereco { get; set; }
        public List<Item> itens { get; set; }
        public string referencia { get; set; }
        public string notificacao { get; set; }
        public DateTime? vencimento { get; set; }
        public Boleto boleto { get; set; }
        public DateTime? recebimento { get; set; }
        public string status { get; set; }
        public List<historico> historico { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public decimal recebido { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public decimal tarifa { get; set; }

    }
    public class historico {
        public string status { get; set; }
        public DateTime? data { get; set; }
    }
}