using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public decimal vlr { get; set; }
        public string cep { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public virtual cliente_plano cliente_plano { get; set; }
    }
}