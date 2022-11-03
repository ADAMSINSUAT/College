using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodMenuApp.Services
{
    public class CheckIfDeviceIsRegistered
    {
        public async Task CheckIfTableNumberisRegistered()
        {
            IDevice device = DependencyService.Get<IDevice>();
            string tablename = device.GetIdentifier();

            string baseurl = "http://192.168.43.47:8082/api/TableNumber/";

            Uri url = new Uri(baseurl);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = new HttpResponseMessage();

            response = await client.GetAsync(url+tablename);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content).ToString();

                if (result != "Device is not registered")
                {
                    TableNumber.tablenumber = Convert.ToInt32(result);
                }
            }
            else
            {
            }
        }
    }
}
