using System.Collections.Generic;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using System.Linq;
using System;

namespace api_widepay.Services {

    public class ClienteService : IClienteService {
        private BancoContext _db;
        public ClienteService (BancoContext db) {

            _db = db;
        }
        public List<cad_cliente> pegarClientesAtivos() {
            return _db.cad_cliente
                .Where (c => c.dt_inicio > DateTime.Parse ("01/01/0001 00:00:00") && c.dt_fim <= DateTime.Parse ("01/01/0001 00:00:00")).ToList();
        }
    }
}