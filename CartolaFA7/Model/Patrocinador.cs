using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    public class Patrocinador
    {
        public int liga_editorial_id { get; set; }
        public int liga_id { get; set; }
        public int servico_cadun { get; set; }
        public string cor_nome_liga { get; set; }
        public string tipo_ranking { get; set; }
        public string url_link { get; set; }
        public object cards { get; set; }
        public int posicao_inicial { get; set; }
        public string autorizacao_promocao { get; set; }
        public string img_background { get; set; }
        public string img_marca_patrocinador { get; set; }
        public string nome { get; set; }
    }
}
