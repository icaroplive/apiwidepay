using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_widepay.Models.Contas {
    public class fin_movimento {
        [Key]
        public int idfin_movimento { get; set; }

        [ForeignKey ("cad_cliente")]
        public int idcad_descricao { get; set; }

        public DateTime data_pag { get; set; } = DateTime.Parse("01-01-0001");
        public DateTime data_venc { get; set; } = DateTime.Parse("01-01-0001");
        public decimal vlr_cob { get; set; }
        public DateTime data_boleto { get; set; } = DateTime.Parse("01-01-0001");
        public int status_pagamento { get; set; }
        public virtual cad_cliente cad_cliente { get; set; }
        public string idwidepay { get; set; }
        public int idcad_conta { get; set; }
        public string codigo_barra { get; set; }
        public decimal vlr_pag { get; set; }
        public decimal vlr_tarifa { get; set; }
        public string obs { get; set; }

    }
}