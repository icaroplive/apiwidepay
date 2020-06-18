using System;
using System.Linq;
using api_widepay.Entities;
using api_widepay.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Services {
    public class VelocidadeService : IVelocidadeService {
        BancoContext _db;
        public VelocidadeService (BancoContext db) {
            _db = db;
        }
        public void atualizar (int idcad_cliente) {
            var cliente =
                (from cd in _db.cad_cliente

                    join cp in _db.cliente_plano on cd.idcad_cliente equals cp.idcad_cliente

                    join pl in _db.planos on cp.idplano equals pl.id

                    where cd.idcad_cliente == idcad_cliente

                    select new { cliente = cd, cliente_plano = cp, plano = pl }).FirstOrDefault ();
            if (cliente != null) {
                var ew = new MkService ("192.168.250.254");
                if (!ew.Login ("admin", "adminew102030")) {
                    ew.Close ();

                }
                var velocidade = api_widepay.Utils.Plano.retornaVelocidade (5);

                string id = cliente.cliente.cpfcnpj.Replace ("-", "").Replace (".", "");
                ew.Send ("/queue/simple/set");
                ew.Send ("=name=" + id);
                ew.Send (String.Format ("=max-limit={0}/{1}", cliente.plano.upload, cliente.plano.download));
                ew.Send (String.Format ("=burst-limit=0/{1}", cliente.plano.upload, cliente.plano.velocidade_maxima));
                ew.Send (String.Format ("=burst-threshold=0/{0}", cliente.plano.velocidade_minima_burst));
                ew.Send (String.Format ("=burst-time=0/{0}", cliente.plano.tempo_burst));
                ew.Send ("=comment=" + cliente.cliente.nome);
                ew.Send ("=.id=" + cliente.cliente.ip, true);

            }

        }
    }

}