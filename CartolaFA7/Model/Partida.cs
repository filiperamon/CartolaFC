using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    public class Partida
    {
        public int clube_casa_id { get; set; }
        public int clube_casa_posicao { get; set; }
        public int clube_visitante_id { get; set; }
        public List<string> aproveitamento_mandante { get; set; }
        public List<string> aproveitamento_visitante { get; set; }
        public int clube_visitante_posicao { get; set; }
        public string partida_data { get; set; }
        public string local { get; set; }
        public bool valida { get; set; }
        public object placar_oficial_mandante { get; set; }
        public object placar_oficial_visitante { get; set; }
        public string url_confronto { get; set; }
        public string url_transmissao { get; set; }
    }
}
