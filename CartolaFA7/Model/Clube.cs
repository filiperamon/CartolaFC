using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    [DataContract]
    public class Clube
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "nome")]
        public string Nome { get; set; }
        [DataMember(Name = "abreviacao")]
        public string Abreviação { get; set; }
        [DataMember(Name = "posicao")]
        public int Posicao { get; set; }
        [DataMember(Name = "escudos")]
        public Escudos Escudos { get; set; }
    }
}
