using FoodMenuApp.Models;
using FoodMenuApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class ManageTableNumber : ContentPage
    {
        //string baseurl = "http://192.168.43.47:8082/api/";
        AndroidTableNumber _androidTableNumber;
        string username, password, comma;
        string finalurl;
        int tablenumber = TableNumber.tablenumber;

        public ManageTableNumber()
        {
            InitializeComponent();

            lblOldTableNumber.Text = Convert.ToString(tablenumber);
        }
        private async void BtnChangeTableNumber_Clicked(object sender, EventArgs e)
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
                else if(txtNewTableNumber.Text =="")
                {
                    await DisplayAlert("Error Message", "New Table Number is empty", "Ok");
                }
                else if (username == "" && password == "" && txtNewTableNumber.Text =="")
                {
                    await DisplayAlert("Error Message", "All fields are empty!", "Ok");
                }
                else
                {
                    string adminurl = "http://192.168.43.47:8082/api/AdminVerify/";

                    HttpClient client = new HttpClient();

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(adminurl+username+","+password);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var verify = JsonConvert.DeserializeObject(content).ToString();

                        if (verify == "Access Granted")
                        {
                            IDevice device = DependencyService.Get<IDevice>();
                            string tablename = device.GetIdentifier();

                            string savetableurl = "http://192.168.43.47:8082/api/TableNumber";
                            _androidTableNumber = new AndroidTableNumber();
                            _androidTableNumber.TableNo = Convert.ToInt32(txtNewTableNumber.Text);
                            _androidTableNumber.TableDeviceName = Convert.ToString(tablename);
                            String json = JsonConvert.SerializeObject(_androidTableNumber);
                            StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                            var postresponse = await client.PutAsync(savetableurl, stringcontent);
                            if (postresponse.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Status Message:", "Table Number is now updated!", "Ok");
                                TableNumber.tablenumber = Convert.ToInt32(txtNewTableNumber.Text);
                                lblOldTableNumber.Text = Convert.ToString(TableNumber.tablenumber);
                                Clear();
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

        private void Clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtNewTableNumber.Text = "";
        }

        protected override void OnDisappearing()
        {
            Clear();
            base.OnDisappearing();
        }
    }
}