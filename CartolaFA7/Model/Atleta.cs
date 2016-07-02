using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    public class Atleta
    {
        public int atleta_id { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string foto { get; set; }
        public int preco_editorial { get; set; }                        
        public int rodada_id { get; set; }
        public int clube_id { get; set; }
        public int posicao_id { get; set; }
        public int status_id { get; set; }
        public double pontos_num { get; set; }
        public double preco_num { get; set; }
        public double variacao_num { get; set; }
        public double media_num { get; set; }
        public int jogos_num { get; set; }
        public PartidaProxRodada partida { get; set; }
        public Scout scout { get; set; }
        public double pontuacao { get; set; }
    }
}
