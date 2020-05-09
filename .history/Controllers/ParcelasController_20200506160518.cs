using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ParcelasController : ControllerBase {
        private IParcelas _parcelas;
        public ParcelasController (IParcelas parcelas) {
            _parcelas = parcelas;
        }

        [HttpGet ("{id}")]
        public ActionResult<List<fin_movimento>> Get (int id) {
            return _parcelas.pegarParcelas (id);

        }

        
        [HttpPost]
        public ActionResult<planos> Post ([FromBody] List<int> idfin_movimento) {
            //_db.Entry (value).State = EntityState.Detached;
           // _db.planos.Update (value);
            //_db.SaveChanges ();
            //return value;
        }


    }
}