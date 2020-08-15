using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace dBosque.Stub.Frontend.Blazor.Client.Shared
{
    public partial class InstanceComponent : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Parameter]
        public Service Service { get; set; }

        private IEnumerable<Instance> Items;

        private Instance _selected;

        protected override async Task OnParametersSetAsync()
        {
            if (Service != null)
            {
                Items = await Http.GetFromJsonAsync<Instance[]>($"{Service.Link}/instance");
            }
        }

        protected void ChangeSelected(Instance i)
        {
            if (i == _selected)
            {
                _selected = null;
            } else
            {
                _selected = i;
            }
        }
        protected string ValueOrDefault<T>(T data, string def)
        {
            if (data == null)
            {
                return def;
            }
            return data.ToString();
        }

        protected void SaveSelected()
        {

        }
        protected string ConvertResponse(string response, bool trim = true)
        {
            var x = Encoding.Default.GetString(Convert.FromBase64String(response));
            return trim?x.Substring(0, x.Length < 100 ? x.Length : 100):x;
        }
    }
}
