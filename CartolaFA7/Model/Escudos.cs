using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    [DataContract]
    public class Escudos
    {
        [DataMember(Name = "60x60")]
        public string Url_60_X_60 { get; set; }
        [DataMember(Name = "45x45")]
        public string Url_45_X_45 { get; set; }
        [DataMember(Name = "30x30")]
        public string Url_30_X_30 { get; set; }
    }
}
