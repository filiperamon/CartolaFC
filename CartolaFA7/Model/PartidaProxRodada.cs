using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    [DataContract]
    public class PartidaProxRodada
    {
        [DataMember(Name = "clube_casa_id")]
        public int ClubeMandanteId { get; set; }
        [DataMember(Name = "clube_casa_posicao")]
        public int ClubeCasaPosição { get; set; }
        [DataMember(Name = "clube_visitante_id")]
        public int ClubeVisitanteId { get; set; }
        [DataMember(Name = "clube_visitante_posicao")]
        public int ClubeVisitantePosicao { get; set; }
        [DataMember(Name = "partida_data")]
        public string DataPartida { get; set; }
        [DataMember(Name = "local")]
        public string localPartida { get; set; }
    }
}
