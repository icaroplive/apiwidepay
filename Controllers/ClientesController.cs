using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_widepay.Entities;
using api_widepay.Models.Contas;
using api_widepay.Services;
using api_widepay.Interfaces;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase {
        private BancoContext _db;
        private IVelocidadeService _velocidade;
        public ClientesController (BancoContext db, IVelocidadeService velocidadeService) {
            _db = db;
            _velocidade = velocidadeService;
        }

        [HttpGet]
        public ActionResult<List<cad_cliente>> Get () {
            var ew = _db.cad_cliente
                //.Where (c => c.dt_inicio > DateTime.Parse ("01/01/0001 00:00:00") && c.dt_fim <= DateTime.Parse ("01/01/0001 00:00:00"))
                .Include ("cliente_plano")
                .Include ("cliente_plano.planos")
                .ToList ();

            return ew;

        }
        [HttpPost]
        public ActionResult<cad_cliente> Post ([FromBody] cad_cliente value) {
            //value.cliente_plano.planos = null;
            //_db.Entry (value).State = EntityState.Detached;
            //_db.Remove(_db.planos.Where(p => p.id == value.cliente_plano.idplano).FirstOrDefault());
            _db.cad_cliente.Add (value);
            _db.SaveChanges ();
            //value.cliente_plano.planos = _db.planos.Where(p => p.id == value.cliente_plano.idplano).FirstOrDefault();
            _velocidade.atualizar(value.idcad_cliente);
            return value;
        }
        [HttpPut]
        public ActionResult<cad_cliente> Put ([FromBody] cad_cliente value) {
            //value.cliente_plano.planos = null;
            //_db.Entry (value).State = EntityState.Detached;
            //_db.Remove(_db.planos.Where(p => p.id == value.cliente_plano.idplano).FirstOrDefault());
            _db.cad_cliente.Update (value);
            _db.SaveChanges ();
            //value.cliente_plano.planos = _db.planos.Where(p => p.id == value.cliente_plano.idplano).FirstOrDefault();
            _velocidade.atualizar(value.idcad_cliente);
            return value;
        }

    }
}