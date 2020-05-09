using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mk_bk.Models {
    public class cliente_plano {
        [Key]
        public int id { get; set; }

        public int idcad_cliente { get; set; }

        [ForeignKey ("planos")]
        public int? idplano { get; set; }

        public virtual planos planos { get; set; }
    }
}