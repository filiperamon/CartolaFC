using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    class Time
    {
        public int time_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string facebook_id { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string foto_perfil { get; set; }
        public bool assinante { get; set; }
    }
}
