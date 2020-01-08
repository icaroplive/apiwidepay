using System.Collections.Generic;

namespace api_widepay.Models.Retorno
{
    public class RetornoCobrancas
    {
        public bool sucesso { get; set; }
        public int total { get; set; }
        public List<Cobranca> cobrancas { get; set; }
    }
}