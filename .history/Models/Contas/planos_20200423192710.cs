using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mk_bk.Models {
    public class planos {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string download { get; set; }
        public string upload { get; set; }
        public string velocidade_minima_burst { get; set; }
        public string velocidade_maxima { get; set; }
        public string tempo_burst { get; set; }

    }
}