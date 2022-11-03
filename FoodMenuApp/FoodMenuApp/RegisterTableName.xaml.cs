using FoodMenuApp.Models;
using FoodMenuApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterTableName : ContentPage
    {
        string username, password;
        private AndroidTableNumber _androidTableNumber;
        public RegisterTableName()
        {
            InitializeComponent();
        }

        private async void BtnSaveTableNumber_Clicked(object sender, EventArgs e)
        {
            try
            {
                username = txtUsername.Text;
                password = txtPassword.Text;

                if (username == "")
                {
                    await DisplayAlert("Error Message", "Username is empty", "Ok");
                }
                else if (password == "")
                {
                    await DisplayAlert("Error Message", "Password is empty", "Ok");
                }
                else if (username == "" && password == "")
                {
                    await DisplayAlert("Error Message", "Username and Password is empty", "Ok");
                }
                else
                {
                    string baseurl = "http://192.168.43.47:8082/api/AdminVerify/";

                    Uri url = new Uri(baseurl+username+","+password);

                    HttpClient client = new HttpClient();

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var verify = JsonConvert.DeserializeObject(content).ToString();

                        if(verify == "Access Granted")
                        {
                            IDevice device = DependencyService.Get<IDevice>();
                            string tablename = device.GetIdentifier();

                            string savetableurl = "http://192.168.43.47:8082/api/TableNumber";
                            _androidTableNumber = new AndroidTableNumber();
                            _androidTableNumber.TableNo = Convert.ToInt32(txtTableNumber.Text);
                            _androidTableNumber.TableDeviceName = Convert.ToString(tablename);
                            String json = JsonConvert.SerializeObject(_androidTableNumber);
                            StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                            var postresponse = await client.PostAsync(savetableurl, stringcontent);
                            if (postresponse.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Status Message:", "Device is now registered and Table Number is now set. This app will now exit.", "Ok");
                                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                            }
                            else
                            {
                                await DisplayAlert("Status Message:", "Request could not be completed, please try again.", "Ok");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Confirmation Message", "Incorrect Password or Username. Please check!", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error Message", "Cannot send request to server", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }
    }
}