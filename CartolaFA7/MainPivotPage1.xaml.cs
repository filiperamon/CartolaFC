using System;
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

namespace CartolaFA7.View
{
    public partial class MainPivotPage1 : PhoneApplicationPage
    {
        private int mes;
        public MainPivotPage1()
        {
            InitializeComponent();
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

               foreach(Jogador j in res)
               {
                   listJogadores.Items.Add(j.Atleta.nome + "\n" 
                       +"Apelido: "+ j.Atleta.apelido + "\n"
                       + "Cartoletas: " + j.Atleta.preco_editorial + "\n"
                       + "Escalações: " + j.escalacoes + "\n"
                       + "Clube: " + j.clube + "\n"
                       + "Posição: " + j.posicao + "\n"
                       );
               }
        }
    }
}