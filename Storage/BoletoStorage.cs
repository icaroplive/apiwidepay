using System.IO;
using api_widepay.Interfaces;

namespace api_widepay.Storage {
    public class BoletoStorage : IBoletoStorage {
        public void gravarTxt (int idfin_movimento, string codigo) {
            string filePath = @"/var/www/api.ipr.net.br/segunda-via/boletos/" + idfin_movimento + ".txt";
            using (StreamWriter outputFile = new StreamWriter (filePath)) {
                outputFile.WriteLine (codigo);
            }
        }
    }
}