using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    class DetalhesTime
    {
        public List<Atleta> atletas { get; set; }
        public Dictionary<int, Clube> clubes { get; set; }
        public Dictionary<int, Posicoes> posicoes { get; set; }
        public Dictionary<int, Status> status { get; set; }
        public TimeDetalhes time { get; set; }
        public float patrimonio { get; set; }
        public int esquema_id { get; set; }
        public float pontos { get; set; }
        public float valor_time { get; set; }
        public int rodada_atual { get; set; }
    }
}
