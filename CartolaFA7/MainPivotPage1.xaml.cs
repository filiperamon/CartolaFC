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
using System.Threading.Tasks;

namespace CartolaFA7.View
{
    public partial class MainPivotPage1 : PhoneApplicationPage
    {
        private int mes;
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

        private void btnStatusMercado_Click(object sender, RoutedEventArgs e)
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
         /*   if(res.fechamento.dia < 10)
            {
                string conc = "0" + res.fechamento.dia;
                res.fechamento.dia = Convert.ToInt32(conc);
            }
            if (res.fechamento.mes < 10)
            {
                string conc = "0" + res.fechamento.mes;
                mes = Convert.ToInt32(conc);
                MessageBox.Show("" + mes);
            }*/
            lblStatusMercado.Text = String.Format("Rodada Atual = {0}\nTimes Escalados = {1}\nData de Fechamento = {2}/{3}/{4}\nHora de Fechamento= {5}:{6}",
                    res.rodada_atual, res.times_escalados, res.fechamento.dia.ToString("00"), res.fechamento.mes.ToString("00"), res.fechamento.ano, 
                    res.fechamento.hora.ToString("00"), res.fechamento.minuto.ToString("00"));
        }

        private void btnListarJogadores_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += Client_ListarJogadores; ;
            Uri uri = new Uri("https://api.cartolafc.globo.com/mercado/destaques", UriKind.Absolute);
            client.OpenReadAsync(uri);
        }

        private void Client_ListarJogadores(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Jogador>));
            var res = (List<Jogador>)serializer.ReadObject(e.Result);

            listJogadores.ItemsSource = res;

           /*    foreach(Jogador j in res)
               {
                   listJogadores.Items.Add(j.Atleta.nome + "\n" 
                       +"Apelido: "+ j.Atleta.apelido + "\n"
                       + "Cartoletas: " + j.Atleta.preco_editorial + "\n"
                       + "Escalações: " + j.escalacoes + "\n"
                       + "Clube: " + j.clube + "\n"
                       + "Posição: " + j.posicao + "\n"
                       );
               }*/
        }

        private void btnJogadores_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += client_OpenReadCompletedJogadoresParticipantes;
            Uri uri = new Uri("https://api.cartolafc.globo.com/atletas/mercado", UriKind.Absolute);
            client.OpenReadAsync(uri);
        }

        private void client_OpenReadCompletedJogadoresParticipantes(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListaAtletas));
            var res = (ListaAtletas)serializer.ReadObject(e.Result);

            foreach (var item in res.atletas)
            {
                listJogadoresParticipantes.Items.Add(item.nome + "\n"
                    + "Apelido: " + item.apelido + "\n\n"                                     
                    );
            }

        }

        private void PegarRodadasCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IList<Rodada>));
            var res = (IList<Rodada>)serializer.ReadObject(e.Result);
            Rodada rodada = res.ElementAt(0);

            listRodadas.ItemsSource = res;

            //foreach (var item in res)
            //{
               // listRodadas.Items.Add(string.Format("{0}) Inicio: {1}  Fim: {2}",item.rodada_id , item.inicio, item.fim));
            //}

           
            //tbInicio.Text = rodada.inicio;
            //tbFim.Text = rodada.fim;
           
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += PegarRodadasCompleted;
            Uri uri = new Uri("https://api.cartolafc.globo.com/rodadas", UriKind.Absolute);
            client.OpenReadAsync(uri);
        }

        private void btnPatrocinadores_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += Client_OpenReadCompletedPatrocinadores;
            Uri uri = new Uri("https://api.cartolafc.globo.com/patrocinadores", UriKind.Absolute);
            client.OpenReadAsync(uri);        
        }

        private void Client_OpenReadCompletedPatrocinadores(object sender, OpenReadCompletedEventArgs e) 
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Patrocinador>));
            var res = (List<Patrocinador>)serializer.ReadObject(e.Result);
            foreach (var item in res)
            {
                ListboxPatrocinadores.Items.Add(item.nome);    
            }                                    
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string criterioBusca = tbNomeTime.Text;

            if (string.IsNullOrEmpty(criterioBusca))
            {
                MessageBox.Show("Informe um critério para busca.");
                tbNomeTime.Focus();
                return;
            }
            
            WebClient times = new WebClient();
            times.OpenReadCompleted += Times_OpenReadCompleted; ;
            Uri uri = new Uri("https://api.cartolafc.globo.com/times?q=" + criterioBusca, UriKind.Absolute);
            times.OpenReadAsync(uri);
        }

        private void Times_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            List<Time> times = JsonConvert.DeserializeObject<List<Time>>(new StreamReader(e.Result).ReadToEnd());

            listTimes.ItemsSource = times;
        }

        private void stkTime_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Time time = (Time)(sender as FrameworkElement).DataContext;

            NavigationService.Navigate(new Uri("/DetalhesTime.xaml?slugTime=" + time.slug, UriKind.Relative));

        }
    }
}