using System.Collections.Generic;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;

namespace api_widepay.Repository
{
    public class FinanceiroRepository : IParcelas
    {
        public List<fin_movimento> pegarParcelas(int idcad_descricao)
        {
            throw new System.NotImplementedException();
        }

        public void removerParcelas(List<int> parcelas)
        {
            throw new System.NotImplementedException();
        }
    }
}