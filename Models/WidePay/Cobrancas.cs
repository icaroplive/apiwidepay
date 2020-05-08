using System;
using System.Collections.Generic;

namespace api_widepay.Models.WidePay
{
    public class Cobrancas
    {
        public bool sucesso { get; set; }
        public int total { get; set; }
        public List<cob> cobrancas { get; set; }
    }
    public class cob {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime? vencimento { get; set; }
        public DateTime? recebimento { get; set; }
        public string cliente { get; set; }
    }
}