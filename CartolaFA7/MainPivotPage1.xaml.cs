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
using System.Windows.Media.Imaging;

namespace CartolaFA7.View
{
    public partial class MainPivotPage1 : PhoneApplicationPage
    {
        String slugMito = null;
        public MainPivotPage1()
        {
            InitializeComponent();

            WebClient partida = new WebClient();
            partida.OpenReadCompleted += Partida_OpenReadCompleted;
            Uri uri = new Uri("https://api.cartolafc.globo.com/partidas", UriKind.Absolute);
            partida.OpenReadAsync(uri);
        }
        private void AtualizaListaClubes(IEnumerable<Clube> clubes)
        {
            listClubesParticipantes.ItemsSource = clubes;
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
     
            lblStatusMercado.Text = String.Format("Rodada Atual = {0}\nTimes Escalados = {1}\nData de Fechamento = {2}/{3}/{4}\nHora de Fechamento= {5}:{6}",
                    res.rodada_atual, res.times_escalados, res.fechamento.dia.ToString("00"), res.fechamento.mes.ToString("00"), res.fechamento.ano, 
                    res.fechamento.hora.ToString("00"), res.fechamento.minuto.ToString("00"));

            brdBrush.Visibility = System.Windows.Visibility.Visible; 
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
            List<Destaques> destaques = JsonConvert.DeserializeObject<List<Destaques>>(new StreamReader(e.Result).ReadToEnd());

            listJogadores.ItemsSource = destaques;
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

            var atletas = JsonConvert.DeserializeObject<ListaAtletas>(new StreamReader(e.Result).ReadToEnd());
            var sorted = atletas.atletas.OrderBy(m => m.nome.Trim()).ThenBy(m => m.apelido.Trim()).ToList();

            var status = atletas.status;
            var times = atletas.clubes;
            var posicoes = atletas.posicoes;

            foreach (var atleta in sorted)
            {
                var nomeStatus = status[atleta.status_id].nome;
                atleta.Status = nomeStatus.Equals("Nulo") ? "Não relacionado" : nomeStatus;
                atleta.ClubeNome = times[atleta.clube_id].Nome;
                atleta.Posicao = posicoes[atleta.posicao_id].nome;
            }
            
            listJogadoresParticipantes.ItemsSource = sorted;

            //foreach (var item in res.atletas)
            //{
            //    listJogadoresParticipantes.Items.Add(item.nome + "\n"
            //        + "Apelido: " + item.apelido + "\n\n"                                     
            //        );
            //}

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

        private void btnMitoRodada_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += MitoRodada_OpenReadCompleted1; ; ;
            Uri uri = new Uri("https://api.cartolafc.globo.com/pos-rodada/destaques", UriKind.Absolute);
            client.OpenReadAsync(uri);
        }

        private void MitoRodada_OpenReadCompleted1(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RootObject));
            RootObject res = (RootObject)serializer.ReadObject(e.Result);

            txtMitoRodada.Text = res.mito_rodada.nome_cartola;
            imageFotoPerfil.Source = new BitmapImage(new Uri(res.mito_rodada.foto_perfil, UriKind.Absolute));
            txtTimeMito.Text = res.mito_rodada.nome;
            imageFotoTime.Source = new BitmapImage(new Uri(res.mito_rodada.url_escudo_png, UriKind.Absolute));
            txtMediaCartoletas.Text = String.Format("Cartoletas: {0}\nPontos: {1}",
                res.media_cartoletas.ToString("00.00"), res.media_pontos.ToString("00.00"));
            slugMito = res.mito_rodada.slug;
            btnDetalhesTime.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnDetalhesTime_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DetalhesTime.xaml?slugTime=" + slugMito, UriKind.Relative));
        }

        private void btnClubes_Click(object sender, RoutedEventArgs e)
        {
             WebClient clube = new WebClient();
             clube.OpenReadCompleted += Clube_OpenReadCompleted; ;
             Uri uri = new Uri("https://api.cartolafc.globo.com/partidas", UriKind.Absolute);
             clube.OpenReadAsync(uri);
        }

        private void Clube_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Partidas res = JsonConvert.DeserializeObject<Partidas>(new StreamReader(e.Result).ReadToEnd());
            List<Clube> listaClubes = new List<Clube>();

            foreach (var clubeHash in res.clubes)
            {

                Clube clube = new Clube();
                clube.Nome = clubeHash.Value.Nome;
                clube.Posicao = clubeHash.Value.Posicao;
                clube.Escudos = clubeHash.Value.Escudos;
                

                listaClubes.Add(clube);
            }

            AtualizaListaClubes(listaClubes.OrderBy(c => c.Posicao));
        }

        private void btnPontuacao_Click(object sender, RoutedEventArgs e)
        {
            WebClient clube = new WebClient();
            clube.OpenReadCompleted += Clube_OpenReadCompleted1;
            Uri uri = new Uri("https://api.cartolafc.globo.com/atletas/pontuados", UriKind.Absolute);
            clube.OpenReadAsync(uri);
        }

        private void Clube_OpenReadCompleted1(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                RetornoPosicaoDaRodada res = JsonConvert.DeserializeObject<RetornoPosicaoDaRodada>(new StreamReader(e.Result).ReadToEnd());
                if (res.atletas.Count > 0)
                {
                    List<PosicaoRodada> posicoesRodada = new List<PosicaoRodada>();

                    foreach (var atleta in res.atletas)
                    {
                        PosicaoRodada posicaoRodada = new PosicaoRodada();

                        posicaoRodada.apelido = atleta.Value.apelido;
                        posicaoRodada.pontuacao = atleta.Value.pontuacao;
                        posicaoRodada.clube = res.clubes.Where(p => p.Value.id == atleta.Value.clube_id).FirstOrDefault().ToString();
                        posicaoRodada.posicao = res.posicoes.Where(p => p.Value.id == atleta.Value.posicao_id).FirstOrDefault().ToString();

                        posicoesRodada.Add(posicaoRodada);
                    }

                    pontuacao.ItemsSource = posicoesRodada;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Aguarde! Parciais não estão disponíveis.");
            }
        }
    }
}