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

        [HttpGet("{id}")]
        public ActionResult<byte[]> Get (string id) {
            return _graph.pegarGrafico(id);

        }


    }
}