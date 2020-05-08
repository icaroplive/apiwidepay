using System;
using System.ComponentModel.DataAnnotations;

namespace api_widepay.Models.Contas {
    public class cad_cliente {
        [Key]
        public int idcad_cliente { get; set; }
        public string cpfcnpj { get; set; }
        public string ip { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime? dt_inicio { get; set; }
        public DateTime? dt_fim { get; set; }
        public virtual cliente_plano cliente_plano { get; set; }
    }
}