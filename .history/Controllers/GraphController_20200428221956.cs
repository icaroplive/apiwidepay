using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_widepay.Entities;
using api_widepay.Models.Contas;
using api_widepay.Interfaces;

namespace api_widepay.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase {
        private IGraph _graph;
        public GraphController (IGraph graph) {
            _graph = graph;
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


    }
}