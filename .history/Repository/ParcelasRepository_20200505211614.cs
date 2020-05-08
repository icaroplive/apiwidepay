using System.Collections.Generic;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using System.Linq;
namespace api_widepay.Repository {
    public class ParcelasRepository : IParcelas {
        BancoContext _db;

        public ParcelasRepository (
            BancoContext db

        ) {

            _db = db;
        }
        public List<fin_movimento> pegarParcelas (int idcad_descricao) {
            return _db.fin_movimento.Where(f => f.idcad_descricao == idcad_descricao).ToList();
        }

        public void removerParcelas (List<int> parcelas) {
            throw new System.NotImplementedException ();
        }
    }
}