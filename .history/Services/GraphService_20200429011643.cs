using System.Net;
using api_widepay.Interfaces;

namespace api_widepay.Services {
    public class GraphService : IGraph {
        public byte[] pegarGrafico (string cpf,string graph) {
            var webClient = new WebClient ();
            byte[] imageBytes = webClient.DownloadData (string.Format("http://192.168.250.254/graphs/queue/{0}/{1}.gif",cpf,graph));
            return imageBytes;
        }
    }
}