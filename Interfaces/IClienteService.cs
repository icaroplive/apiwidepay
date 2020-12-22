using System.Collections.Generic;
using api_widepay.Models.Contas;

namespace api_widepay.Interfaces {
    public interface IClienteService {
        List<cad_cliente> pegarClientesAtivos ();
    }
}