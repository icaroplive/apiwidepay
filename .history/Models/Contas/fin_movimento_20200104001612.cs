using System;
using System.ComponentModel.DataAnnotations;

namespace api_widepay.Models.Contas {
    public class fin_movimento {
        [Key]
        public int idfin_movimento { get; set; }
        public string idwidepay { get; set; }
        public int idcad_conta { get; set; }
        public string codigo_barra { get; set; }
        public int idcad_descricao { get; set; }
        public decimal vlr_tarifa { get; set; }
        public DateTime? data_pag { get; set; }
        public int status_pagamento { get; set; }

    }
}