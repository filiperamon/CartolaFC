using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.IO;
using CartolaFA7.Model;

namespace CartolaFA7
{
    public partial class DetalhesTime : PhoneApplicationPage
    {
        public DetalhesTime()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string slug = NavigationContext.QueryString["slugTime"];

            WebClient detalhesTime = new WebClient();
            detalhesTime.OpenReadCompleted += DetalhesTime_OpenReadCompleted;
            Uri uri = new Uri("https://api.cartolafc.globo.com/time/" + slug, UriKind.Absolute);
            detalhesTime.OpenReadAsync(uri);
        }

        private void DetalhesTime_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Model.DetalhesTime detalhesTime = JsonConvert.DeserializeObject<Model.DetalhesTime>(new StreamReader(e.Result).ReadToEnd());

            this.DataContext = detalhesTime;

            List<DetalhesAtleta> listaDetalhesAtletas = new List<DetalhesAtleta>();
            foreach(var atleta in detalhesTime.atletas)
            {
                DetalhesAtleta detalhesAtleta = new DetalhesAtleta();
                detalhesAtleta.apelido = atleta.apelido;
                detalhesAtleta.nome = atleta.nome;
                detalhesAtleta.foto = atleta.foto;
                detalhesAtleta.url_escudo_time = detalhesTime.clubes.Where(c => c.Value.id == atleta.clube_id).FirstOrDefault().Value.Escudos.Url_60_X_60;
                detalhesAtleta.nome_clube = detalhesTime.clubes.Where(c => c.Value.id == atleta.clube_id).FirstOrDefault().Value.Nome;
                detalhesAtleta.posicao = detalhesTime.posicoes.Where(pos => pos.Value.id == atleta.posicao_id).FirstOrDefault().Value.nome;
                detalhesAtleta.status = detalhesTime.status.Where(s => s.Value.id == atleta.status_id).FirstOrDefault().Value.nome;
                detalhesAtleta.pontos = atleta.pontos_num;
                detalhesAtleta.preco = atleta.preco_num;
                detalhesAtleta.jogos = atleta.jogos_num;

                listaDetalhesAtletas.Add(detalhesAtleta);
            }

            listAtletas.ItemsSource = listaDetalhesAtletas;
        }
    }
}