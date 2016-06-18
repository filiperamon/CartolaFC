using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace CartolaFA7.Controller
{
    public class WSConsumer
    {
        /*    public async Task<List<string>> MercadoStatus()
            {
                var repositories = new List<string>();

                string url = "https://api.cartolafc.globo.com/mercado/status";
                Uri uri = new Uri(url, UriKind.Absolute);

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var jsonRepositories = JArray.Parse(await response.Content.ReadAsStringAsync());

                    foreach(var repository in jsonRepositories)
                    {
                        repositories.Add(
                            repository.Value<string>("rodada_atual") + "\n"+
                            repository.Value<string>("times_escalados"));
                    }
                }

                return repositories;

            }*/
    }
}
