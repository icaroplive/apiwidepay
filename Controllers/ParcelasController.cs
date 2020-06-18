using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Models.Contas;
using api_widepay.Models.Retorno;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Controllers {
    //  [Route ("api/[controller]")]
    [ApiController]
    public class ParcelasController : ControllerBase {
        private IParcelas _parcelas;
        public ParcelasController (IParcelas parcelas) {
            _parcelas = parcelas;
        }

        [Route ("api/parcelas/{id}")]
        [HttpGet ("{id}")]
        public ActionResult<List<fin_movimento>> Get (int id) {
            return _parcelas.pegarParcelas (id);

        }

        [Route ("api/cancelarERemoverParcelas")]
        [HttpDelete]
        public ActionResult<RetornoCancela> cancelarERemoverParcelas ([FromBody] List<int> idfin_movimento) {
            return _parcelas.cancelarEremoverParcelas (idfin_movimento);

        }

        [Route ("api/removerParcelas")]
        [HttpDelete]
        public ActionResult<bool> removerParcelas ([FromBody] List<int> idfin_movimento) {
            return _parcelas.removerParcelas (idfin_movimento);

        }

    }
}