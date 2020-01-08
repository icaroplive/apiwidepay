using System;
using System.Collections.Generic;

namespace api_widepay.Models.WidePay
{
    public class Notificacao
    {
        public bool sucesso { get; set; }
        public cobranca cobranca { get; set; }
    }
    public class cobranca {
        public string id { get; set; }
        public DateTime? recebimento { get; set; }
        public string status { get; set; }
    }
    public class historico {
        public string status { get; set; }
        public DateTime? data { get; set; }
    }

}