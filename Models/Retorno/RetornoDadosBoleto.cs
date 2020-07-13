using System.Collections.Generic;

namespace api_widepay.Models.Retorno
{
    public class RetornoDadosBoleto
    {
        public string codigo { get; set; }
        public string html { get; set; }
        public bool sucesso { get; set; }
        public string cobranca { get; set; }
        public int idfin_movimento { get; set; }
        public string erro { get; set; }
        public List<RetornoValidacao> validacao { get; set; }
    }
}