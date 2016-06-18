using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    [DataContract]
    public class Partidas
    {
        [DataMember(Name = "partidas")]
        public List<PartidaProxRodada> partidas { get; set; }
        [DataMember(Name = "clubes")]
        public Dictionary<int, Clube> clubes { get; set; }
        //public List<KeyValuePair<int, Clube>> clubes { get; set; }
    }
}
