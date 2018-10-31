using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AllegroWebService.WebApiService;
using eBay.Service.Core.Soap;
using Microsoft.AspNetCore.Mvc;

namespace CoinsOrganizer.Controlers
{
    public class EbayController : Controller
    {
        public IActionResult Index()
        {
            return View("EbayPage");
        }

        public IActionResult TestEbayApi()
        {




            //var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
            //{
            //    MaxReceivedMessageSize = 15000000 // You might have to adjust this depending on how much data the API calls return
            //};

            //var client = new ebay (binding, new EndpointAddress("https://api.ebay.com/wsapi"));
            //string endpoint = "https://api.sandbox.ebay.com/wsapi";
            //string callName = "GeteBayOfficialTime";
            //string siteId = "0";
            //string appId = "yourAppId";     // use your app ID
            //string devId = "yourDevId";     // use your dev ID
            //string certId = "yourCertId";   // use your cert ID
            //string version = "405";
            //// Build the request URL
            //string requestURL = endpoint
            //                    + "?callname=" + callName
            //                    + "&siteid=" + siteId
            //                    + "&appid=" + appId
            //                    + "&version=" + version
            //                    + "&routing=default";
            //// Create the service
            //eBayAPIInterfaceService service = new eBayAPIInterfaceService();
            //// Assign the request URL to the service locator.
            ////service. = requestURL;
            //// Set credentials
            //service.RequesterCredentials = new CustomSecurityHeaderType();
            //service.RequesterCredentials.eBayAuthToken = "yourToken";    // use your token
            //service.RequesterCredentials.Credentials = new UserIdPasswordType();
            //service.RequesterCredentials.Credentials.AppId = appId;
            //service.RequesterCredentials.Credentials.DevId = devId;
            //service.RequesterCredentials.Credentials.AuthCert = certId;
            //// Make the call to GeteBayOfficialTime
            //GeteBayOfficialTimeRequestType request = new GeteBayOfficialTimeRequestType();
            //request.Version = "405";
            //GeteBayOfficialTimeResponseType response = service.GeteBayOfficialTime(request);
            //Console.WriteLine("The time at eBay headquarters in San Jose, California, USA, is:");
            //Console.WriteLine(response.Timestamp);
            return View("EbayPage");
        }
    }
}