namespace api_widepay.Models.Retorno
{
    public class RetornoDadosBoleto
    {
        public string codigo { get; set; }
        public string html { get; set; }
        public bool sucesso { get; set; }
        public string cobranca { get; set; }
    }
}