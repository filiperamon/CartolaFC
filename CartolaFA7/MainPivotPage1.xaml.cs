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
using System.Threading.Tasks;

namespace CartolaFA7.View
{
    public partial class MainPivotPage1 : PhoneApplicationPage
    {
        public MainPivotPage1()
        {
            InitializeComponent();
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
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListaPatrocinadores));
            ListaPatrocinadores res = (ListaPatrocinadores)serializer.ReadObject(e.Result);
            foreach (var item in res.listaPatrocinador)
            {
                ListboxPatrocinadores.Items.Add(item.nome);    
            }                                    
        }
    }
}