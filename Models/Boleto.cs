using System.Collections.Generic;

namespace api_widepay.Models
{
    public class Boleto
    {
        public decimal desconto { get; set; }
        public decimal multa { get; set; }
        public decimal juros { get; set; }
        public string[] instrucoes { get; set; }   
    }
}