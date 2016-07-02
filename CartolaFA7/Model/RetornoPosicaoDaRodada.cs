using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    [DataContract]
    class RetornoPosicaoDaRodada
    {
        [DataMember]
        public int rodada { get; set; }
        [DataMember]
        public Dictionary<int, Atleta> atletas { get; set; }
        [DataMember]
        public Dictionary<int, Clube> clubes { get; set; }
        [DataMember]
        public Dictionary<int, Posicoes> posicoes { get; set; }
    }
}
