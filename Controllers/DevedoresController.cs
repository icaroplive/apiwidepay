using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Models;
using api_widepay.Models.Contas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevedoresController : ControllerBase
    {
        private BancoContext _db;
        public DevedoresController(BancoContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<Devedor>> Get()
        {
            var dataHoje = DateTime.Now.Date;
            /*var ew = _db.fin_movimento
                .Where (c => c.data_venc > DateTime.Parse ("01/01/0001 00:00:00") && c.data_pag <= DateTime.Parse ("01/01/0001 00:00:00") && dataHoje > c.data_venc)
                .Include ("cad_cliente")
               // .Include ("cliente_plano.planos")
                .ToList ();*/
            return _db.fin_movimento
            .Join(_db.cad_cliente,
             fin => fin.idcad_descricao,
             cliente => cliente.idcad_cliente,
              (fin, cliente) => new { Financeiro = fin, Cliente = cliente })
            .Where(c => c.Financeiro.data_venc > DateTime.Parse("01/01/0001 00:00:00") && c.Financeiro.data_pag <= DateTime.Parse("01/01/0001 00:00:00") && dataHoje > c.Financeiro.data_venc && c.Financeiro.tipo_movimento == 2).ToList()
            .GroupBy(c => c.Financeiro.idcad_descricao)
            .Select(c => new Devedor
            {
                nome = c.FirstOrDefault().Cliente.nome,
                quantidade = c.Count(),
                valor_total = c.Sum(c => c.Financeiro.vlr_cob)

            })
            .ToList();



            //   return ew;

        }

    }
}