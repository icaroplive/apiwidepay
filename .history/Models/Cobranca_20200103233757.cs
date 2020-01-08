using System;
using System.Collections.Generic;

namespace api_widepay.Models {
    public class Cobranca {
        public Cobranca () {
            this.forma = "Boleto";
            this.notificacao = "https://ipr.net.br/segunda-via/widepay/retorno";
            this.cliente = "Lívia Pontarolo Almeida";
            this.pessoa = "Física";
            this.cpf = "463.384.662-02";
        }
        public string forma { get; set; }
        public string cliente { get; set; }
        public string pessoa { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public Endereco endereco { get; set; }
        public List<Item> itens { get; set; }
        public string referencia { get; set; }
        public string notificacao { get; set; }
        public DateTime? vencimento { get; set; }
        public Boleto boleto { get; set; }
        public DateTime? recebimento { get; set; }

    }
}