using System.Collections.Generic;
using System.Linq;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
namespace api_widepay.Repository {
    public class ParcelasRepository : IParcelas {
        BancoContext _db;

        public ParcelasRepository (
            BancoContext db

        ) {

            _db = db;
        }
        public List<fin_movimento> pegarParcelas (int idcad_descricao) {
            return _db.fin_movimento.Where (f => f.idcad_descricao == idcad_descricao).OrderByDescending (f => f.data_venc).ToList ();
        }

        public void removerParcelas (List<int> parcelas) {
            throw new System.NotImplementedException ();
        }
    }
}