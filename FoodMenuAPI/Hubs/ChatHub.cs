using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenuAPITest.Hubs
{
    public class ChatHub : Hub
    {

        public static IHubContext<ChatHub> _hubContext;

        public ChatHub(IHubContext<ChatHub> HubContext)
        {
            _hubContext = HubContext;
        }

        //[HubMethodName("getAllOrders")]
        public async static void SendMessage(string user, JsonResult jsonResult)
        {

            await _hubContext.Clients.Client(user).SendAsync("getAllOrders", jsonResult);
            return;
        }

        //public async Task OnConnected(JsonResult jsonResult)
        //{
        //    await Task.Run(async () =>
        //    {
        //        await _hubContext.Clients.All.SendAsync("refreshOrders", jsonResult);
        //    });
        //    return;
        //}

        //public async Task OnDisconnectedAsync(JsonResult jsonResult)
        //{
        //    await Task.Run(async () =>
        //    {
        //        await _hubContext.Clients.All.SendAsync("refreshOrders", jsonResult);
        //    });
        //    return;
        //}


        //public async void SendMessage(JsonResult jsonResult)
        //{
        //    await Clients.All.SendAsync("refreshOrders", jsonResult);
        //}
        //public async Task OnConnected(JsonResult jsonResult)
        //{
        //    await Clients.All.SendAsync("refreshOrders", jsonResult);
        //    await base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
