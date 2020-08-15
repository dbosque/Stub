using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Components;

namespace dBosque.Stub.Frontend.Blazor.Client.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<Service> services;
        private IEnumerable<Service> soap;
        private IEnumerable<Service> rest;
        private IEnumerable<Match> matches;
        private IEnumerable<Template> templates;
        private IEnumerable<Template> templates2;


        [Inject]
        HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            services = await Http.GetFromJsonAsync<Service[]>("Service");
            soap = services.Where(s => !string.IsNullOrEmpty(s.RootNode));
            rest = services.Where(s => string.IsNullOrEmpty(s.RootNode));

            matches = await Http.GetFromJsonAsync<Match[]>("Match");
        }

        async Task OnItemSelect2(object item)
        {
            var service = item as Service;
            templates = null;
            templates2 = await Http.GetFromJsonAsync<Template[]>($"Service/{service.Id}/template");

        }


      

        void AddServiceClick()
        {
            // Add a new service
        }

    }
}
