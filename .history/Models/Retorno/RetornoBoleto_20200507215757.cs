using System.Collections.Generic;

namespace api_widepay.Models.Retorno
{
    public class RetornoBoleto
    {
        public bool sucesso { get; set; }
        public string id { get; set; }
        public string link { get; set; }
        public string erro { get; set; }
        public int idfin_movimento { get; set; }
        public List<RetornoValidacao> validacao { get; set; }
        public List<RetornoExecucao> execucao { get; set; }
    }
}