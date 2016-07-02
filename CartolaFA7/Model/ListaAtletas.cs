using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    public class ListaAtletas
    {
        public List<Atleta> atletas { get; set; }
        public Dictionary<int, Status> status { get; set; }
        public Dictionary<int, Clube> clubes { get; set; }
        public Dictionary<int, Posicoes> posicoes { get; set; }
    }
}
