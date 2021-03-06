﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CartolaFA7.Resources;
using CartolaFA7.Controller;
using CartolaFA7.Model;
using System.Runtime.Serialization.Json;

namespace CartolaFA7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var statusMercado = new WSConsumer();
            var lista = await statusMercado.MercadoStatus();
            repositories.ItemsSource = lista;*/

            WebClient client = new WebClient();
            client.OpenReadCompleted += Client_OpenReadCompleted;
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