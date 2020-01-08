using System.ComponentModel.DataAnnotations;

namespace api_widepay.Models.Contas
{
    public class cad_cliente
    {
        [Key]
        public int idcad_cliente { get; set; }
        public string nome { get; set; }
        public string cpfcnpj { get; set; }
        public decimal vlr { get; set; }
        public int vencimento { get; set; }
    }
}