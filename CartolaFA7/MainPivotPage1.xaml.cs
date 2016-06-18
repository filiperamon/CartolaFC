using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Runtime.Serialization.Json;
using CartolaFA7.Model;
using Newtonsoft.Json;

namespace CartolaFA7.View
{
    public partial class MainPivotPage1 : PhoneApplicationPage
    {
        public MainPivotPage1()
        {
            InitializeComponent();

            WebClient partida = new WebClient();
            partida.OpenReadCompleted += Partida_OpenReadCompleted;
            Uri uri = new Uri("https://api.cartolafc.globo.com/partidas", UriKind.Absolute);
            partida.OpenReadAsync(uri);
        }

        private void atualizaListaJogosRodada(List<ModeloPartida> partidas)
        {
            listaPartidas.ItemsSource = partidas;
        }

        private void Partida_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Partidas res = JsonConvert.DeserializeObject<Partidas>(new StreamReader(e.Result).ReadToEnd());

            List<ModeloPartida> listaPartida = new List<ModeloPartida>();

            foreach (var partida in res.partidas)
            {
                var dataPartida = partida.DataPartida.Split(' ');
                var diaPardida = dataPartida[0].Split('-');

                Clube mandante = res.clubes.FirstOrDefault(c => c.Value.id == partida.ClubeMandanteId).Value;
                Clube visitante = res.clubes.FirstOrDefault(c => c.Value.id == partida.ClubeVisitanteId).Value;

                ModeloPartida modelo = new ModeloPartida();
                modelo.NomeMandante = mandante.Nome;
                modelo.NomeVisitante = visitante.Nome;
                modelo.PosicaoMandante = mandante.Posicao;
                modelo.PosicaoVisitante = visitante.Posicao;
                modelo.UrlEscudoMandante = mandante.Escudos.Url_45_X_45;
                modelo.UrlEscudoVisitante = visitante.Escudos.Url_45_X_45;
                modelo.LocalPartida = partida.localPartida;
                modelo.DataPartida = diaPardida[2] + "/" + diaPardida[1] + "/" + diaPardida[0] + " " + dataPartida[1];

                listaPartida.Add(modelo);

            }

            atualizaListaJogosRodada(listaPartida);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += Client_OpenReadCompleted; ;
            Uri uri = new Uri("https://api.cartolafc.globo.com/mercado/status", UriKind.Absolute);
            client.OpenReadAsync(uri);
        }

        private void Client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StatusMercadoJson));
            StatusMercadoJson res = (StatusMercadoJson)serializer.ReadObject(e.Result);
            lblStatusMercado.Text = String.Format("Rodada Atual={0}\nTimes Escalados={1}\n",
                    res.rodada_atual, res.times_escalados);
        }
    }
}