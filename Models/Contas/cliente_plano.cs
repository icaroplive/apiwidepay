using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_widepay.Models.Contas {
    public class cliente_plano {
        [Key]
        public int id { get; set; }

        public int idcad_cliente { get; set; }

        [ForeignKey ("planos")]
        public int idplano { get; set; }
        public virtual planos planos { get; set; }

    }
}