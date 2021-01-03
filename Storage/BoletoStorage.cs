using System.IO;
using api_widepay.Interfaces;

namespace api_widepay.Storage {
    public class BoletoStorage : IBoletoStorage {
        public void gravarTxt (int idfin_movimento, string codigo) {
            string filePath = @"/var/www/ipr.net.br/segunda-via/boletos/" + idfin_movimento + ".txt";
            
            using (StreamWriter outputFile = new StreamWriter(File.Open(filePath, System.IO.FileMode.Append))) {
                outputFile.WriteLine (codigo.Replace("CPF: 095.441.926-03","CNPJ: 23.418.622/0001-30"));
            }
        }
    }
}