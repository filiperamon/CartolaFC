using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    class ModeloPartida
    {
        public string NomeMandante { get; set; }
        public string NomeVisitante { get; set; }
        public string UrlEscudoMandante { get; set; }
        public string UrlEscudoVisitante { get; set; }
        public string DataPartida { get; set; }
        public string LocalPartida { get; set; }
        public int PosicaoMandante { get; set; }
        public int PosicaoVisitante { get; set; }
    }
}
