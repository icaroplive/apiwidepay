using System.Collections.Generic;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;

namespace api_widepay.Interfaces
{
    public interface IParcelas
    {
         List<fin_movimento> pegarParcelas(int idcad_descricao);
         RetornoCancela removerParcelas(List<int> parcelas);
    }
}