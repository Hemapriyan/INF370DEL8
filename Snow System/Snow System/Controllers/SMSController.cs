using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using Nexmo.Api;
//using Twilio.Clients;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using System.Configuration;

namespace Snow_System.Controllers
{
    public class SMSController : TwilioController
    {
        // GET: SMS
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [System.Web.Mvc.HttpGet]
        public ActionResult Send()
        {
            
            var accountSid = ConfigurationManager.AppSettings["AC91a02b66d1de75a0e7b8b159e26482e0"];
            var authToken = ConfigurationManager.AppSettings["f005c401f04a146c6e8cf93159e995ca"];
            TwilioClient.Init("AC91a02b66d1de75a0e7b8b159e26482e0", "f005c401f04a146c6e8cf93159e995ca");
            
             

            var to = new PhoneNumber("+27794558925");
            var from = new PhoneNumber("+18706690845");

            var message = MessageResource.Create(to: to, from: from, body: "Hi there. This is to inform you that you have requested to reset your password. Please click the link sent to your email. Spartan Fire" );

            return Content(message.Sid);
        }

        //[System.Web.Mvc.HttpPost]
        //public ActionResult Send(string to, string text)
        //{
        //    //WebClient c = new WebClient();
        //    //string baseURl = 

        //    //var results = SMS.Send(new SMS.SMSRequest
        //    //{
        //    //    from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
        //    //    to = to,
        //    //    text = text
        //    //});


        //    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + 0794558925 + "&pwd=" + "" + "&msg=" + "Hi"+ "&phone=" + 0794558925+ "&provider=way2sms");
        //    //HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //    //System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //    //string responseString = respStreamReader.ReadToEnd();
        //    //respStreamReader.Close();
        //    //myResp.Close();
        //    //return View();
        //}
    }
}