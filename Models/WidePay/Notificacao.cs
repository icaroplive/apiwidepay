using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api_widepay.Models.WidePay {
    public class Notificacao {
        public bool sucesso { get; set; }
        public cobranca cobranca { get; set; }
    }
    public class cobranca {
        public string id { get; set; }
        public DateTime recebimento { get; set; } = DateTime.Parse("01-01-0001");

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public decimal recebido { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public decimal tarifa { get; set; }
        public string status { get; set; }
        public List<historico> historico { get; set; }
    }
    public class historico {
        public string status { get; set; }
        public DateTime? data { get; set; }
    }

}