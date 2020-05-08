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
        public ActionResult<List<byte[]>> Get (string id) {
            var graphs = new List<string>(){ "daily", "weekly","monthly","yearly"};
            var lista = new List<byte[]>();
            foreach (var x in graphs) {
                lista.add(_graph.pegarGrafico(new string(id.Where(c => char.IsDigit(c)).ToArray())));
            }
            
          // return _graph.pegarGrafico(new string(id.Where(c => char.IsDigit(c)).ToArray()));
          return lista;

        }


    }
}