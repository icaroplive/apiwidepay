using System.Collections.Generic;
using System.Linq;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
namespace api_widepay.Repository {
    public class ParcelasRepository : IParcelas {
        BancoContext _db;
        IWidePay _widepay;

        public ParcelasRepository (
            BancoContext db,
            IWidePay widepay

        ) {

            _db = db;
            _widepay = widepay;
        }
        public List<fin_movimento> pegarParcelas (int idcad_descricao) {
            return _db.fin_movimento.Where (f => f.idcad_descricao == idcad_descricao).OrderByDescending (f => f.data_venc).ToList ();
        }

        public RetornoCancela removerParcelas (List<int> parcelas) {

            var lista_idwidepay = _db.fin_movimento.Where (f => parcelas.Contains (f.idfin_movimento)).ToList ();

            var lista_cancela = (from x in lista_idwidepay select x.idwidepay ).ToList ();
            
            _widepay.cancelarCobrancas(lista_cancela);

    }
}
}