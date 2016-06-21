using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartolaFA7.Model
{
    class TimeDetalhes
    {
        public int time_id { get; set; }
        public int clube_id { get; set; }
        public int esquema_id { get; set; }
        public int cadun_id { get; set; }
        public string facebook_id { get; set; }
        public string foto_perfil { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public int tipo_escudo { get; set; }
        public string cor_fundo_escudo { get; set; }
        public string cor_borda_escudo { get; set; }
        public string cor_primaria_estampa_escudo { get; set; }
        public string cor_secundaria_estampa_escudo { get; set; }
        public string url_escudo_svg { get; set; }
        public string url_escudo_png { get; set; }
        public string url_camisa_svg { get; set; }
        public string url_camisa_png { get; set; }
        public string url_escudo_placeholder_png { get; set; }
        public int tipo_estampa_escudo { get; set; }
        public int tipo_adorno { get; set; }
        public int tipo_camisa { get; set; }
        public int tipo_estampa_camisa { get; set; }
        public string cor_camisa { get; set; }
        public string cor_primaria_estampa_camisa { get; set; }
        public string cor_secundaria_estampa_camisa { get; set; }
        public string assinante { get; set; }
        public string cadastro_completo { get; set; }
        public int patrocinador1_id { get; set; }
        public int patrocinador2_id { get; set; }
    }
}
