using System.Collections.Generic;
using System.Threading.Tasks;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;

namespace api_widepay.Interfaces {
    public interface IParcelas {
        List<fin_movimento> pegarParcelas (int idcad_descricao);
        RetornoCancela cancelarEremoverParcelas (List<int> parcelas);

        bool removerParcelas (List<int> parcelas);
    }
}