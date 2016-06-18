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
    }
}
