using api_widepay.Models.Contas;

namespace api_widepay.Models
{
    public class Devedor
    {
        public string nome { get; set; }
        public int quantidade { get; set; }
        public decimal valor_total { get; set; }
    }
}