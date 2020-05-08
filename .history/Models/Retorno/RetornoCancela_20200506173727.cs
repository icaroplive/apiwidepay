using System.Collections.Generic;
namespace api_widepay.Models.Retorno {

    public class RetornoCancela {
        public bool sucesso { get; set; }
        public int total { get; set; }
        public string erro { get; set; }
        public List<execucao> execucao { get; set; }
        public List<validacao> validacao { get; set; }
    }
    public class execucao {
        public string id { get; set; }
        public string erro { get; set; }
    }

    public class validacao {
        public string id { get; set; }
        public string erro { get; set; }
    }
}