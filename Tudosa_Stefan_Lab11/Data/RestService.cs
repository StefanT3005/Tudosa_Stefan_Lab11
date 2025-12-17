using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudosa_Stefan_Lab11.Models;

namespace Tudosa_Stefan_Lab11.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        string BaseUrl = "https://localhost:7183/api/ShopLists";

        public List<ShopList> Items { get; private set; }

        public RestService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            client = new HttpClient(handler);
        }

        public async Task<List<ShopList>> RefreshDataAsync()
        {
            Items = new List<ShopList>();
            var response = await client.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<ShopList>>(content);
            }
            return Items;
        }

        public async Task SaveShopListAsync(ShopList item, bool isNewItem)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (isNewItem)
                await client.PostAsync(BaseUrl, content);
            else
                await client.PutAsync($"{BaseUrl}/{item.ID}", content);
        }

        public async Task DeleteShopListAsync(int id)
        {
            await client.DeleteAsync($"{BaseUrl}/{id}");
        }
    }
}
