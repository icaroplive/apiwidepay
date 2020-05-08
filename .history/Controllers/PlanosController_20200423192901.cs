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
    public class PlanosController : ControllerBase {
        private BancoContext _db;
        public PlanosController (BancoContext db) {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<planos>> Get () {
            return _db.planos.ToList ();

        }

        [HttpPut]
        public ActionResult<planos> Put ([FromBody] planos value) {
            //_db.Entry (value).State = EntityState.Detached;
            _db.planos.Update (value);
            _db.SaveChanges ();
            return value;
        }

    }
}