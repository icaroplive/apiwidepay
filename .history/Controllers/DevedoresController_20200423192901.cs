using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mk.Entities;
using mk.Services;
using mk_bk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mk.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class DevedoresController : ControllerBase {
        private BancoContext _db;
        public DevedoresController (BancoContext db) {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<fin_movimento>> Get () {
            var dataHoje = DateTime.Now.Date;
            var ew = _db.fin_movimento
                .Where (c => c.data_venc > DateTime.Parse ("01/01/0001 00:00:00") && c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && dataHoje > c.data_venc)
                .Include ("cad_cliente")
               // .Include ("cliente_plano.planos")
                .ToList ();

            return ew;

        }

    }
}