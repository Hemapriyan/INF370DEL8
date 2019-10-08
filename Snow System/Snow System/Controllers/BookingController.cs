using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            IEnumerable<mvcClientModel> clientList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client").Result;

            clientList = response.Content.ReadAsAsync<IEnumerable<mvcClientModel>>().Result;

            return View(clientList);
        }
    }
}