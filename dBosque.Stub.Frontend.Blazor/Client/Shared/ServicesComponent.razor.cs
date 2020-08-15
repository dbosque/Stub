using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace dBosque.Stub.Frontend.Blazor.Client.Shared
{
    public partial class ServicesComponent : ComponentBase
    {
        private Service _selected = null;

        [Inject]
        HttpClient Http { get; set; }

        [Parameter]
        public string Type{ get; set; }

        [Parameter]
        public IEnumerable<Service> Services { get; set; }

        protected override void OnParametersSet()
        {
           
        }

        async Task OnItemSelect(object item)
        {
            _selected = item as Service;
            Console.WriteLine(_selected);
        }


        void OnAddTemplate(Service service)
        {
        }

        async Task OnDelete(Service service)
        {
            await Http.DeleteAsync(service.Link);
        }

        void OnEdit(Service service)
        {
        }
    }
}
