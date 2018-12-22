using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;

namespace CoinsOrganizerDesktop.MarketService
{
    public static class EbayService
    {
        private static ApiContext _apiContext = null;
        private static DispatcherTimer _timer = null;

        public static ItemTypeCollection ActiveList { get; set; }
        public static OrderTransactionTypeCollection SoldList { get; set; }

        public static void InitializeEbay()
        {
            GetApiContext();

            if (ActiveList == null)
            {
                ActiveList = new ItemTypeCollection();
            }

            if (SoldList == null)
            {
                SoldList = new OrderTransactionTypeCollection();
            }

            if (_timer == null)
            {
                //_timer = new DispatcherTimer(TimeSpan.FromMinutes(1), DispatcherPriority.Background,
                //    RefreshEbayData, Dispatcher.CurrentDispatcher);
                //_timer.Start();
            }
        }

        private static void RefreshEbayData(object sender, EventArgs eventArgs)
        {
            var apicall = new GetMyeBaySellingCall(_apiContext);

            apicall.ActiveList = new ItemListCustomizationType();
            apicall.ActiveList.Sort = ItemSortTypeCodeType.EndTime;
            apicall.SoldList = new ItemListCustomizationType();
            apicall.SoldList.Sort = ItemSortTypeCodeType.EndTime;

            apicall.ActiveList.Pagination = new PaginationType { EntriesPerPage = 100 };
            apicall.SoldList.Pagination = new PaginationType { EntriesPerPage = 100 };

            apicall.GetMyeBaySelling();

            ActiveList = apicall.ActiveListReturn.ItemArray;
            SoldList = apicall.SoldListReturn.OrderTransactionArray;
        }

        private static void GetApiContext()
        {
            if (_apiContext == null)
            {
                _apiContext = new ApiContext();

                _apiContext.SoapApiServerUrl = "https://api.ebay.com/wsapi";
                ApiCredential apiCredential = new ApiCredential();
                apiCredential.eBayToken =
                    "AgAAAA**AQAAAA**aAAAAA**QfmvWw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6ADkoehCJKBqAidj6x9nY+seQ**CbUEAA**AAMAAA**g72igHsAWb44hhUnierXvX8o4YyDEuaXgtczBFz1Rh5R6AC0t9QZhukhCiupG1xD8VE3Ae20Zy2qSo61+GyJuRZjZLZ58zGt0b8RSPsvOHuIWjnNuPUq+6g16N8QQexnG2AMwiGx26GH9BO/AbPEjMWt8bZ3GCocnr8ZZeA5kWPFaGpah/3k8kDc3bC4X4FhQ73x8NJp42UsN/fTfOtPJzpHK9qj7r4344svvO1IjPuDktKE/zPb3lSG+5cPJ3rCKkNPmpwxwRE4hS7gUTHp/AvofiYtyx5GTzs0T4X1DMlloSdIA2vFjOBXIKdAGWfl5oku9yeQICg62io7Mvp+jyMWxvhpNwRRPXVyr0n84R9q9G3gNVcLo8gAeCyjNX26/+Jc980h5vbUz5eUoCn9evVMyXbW/GUu09pIEm/WLk5s5NwQlLhOHaLAqhit7lRPW1LE21nNmno+XY5vdOak4HjOBmq7PaUDjzvYuaZKU0Vn0WI/34TxUzTGRZIRzP1OTs2iwEu5CAVSEOBrpgU47yayY1ClhfUhTPVCVtMh95yQY/qvS3k4LjVfKV6PFfIJF6RFU/+gqHfLZlbie5q7Vu0hTO3xe9pcR+IvCAfoZDX+YhIYcwMn3MXzFBzVs++O2yQ8BBQuOZd+LwjEudtWgDIyWUbMrKvu0idVEmJt9PHHZB/oybJjWc7g5OaKbnm1eG2mATa53VA87pdsoIV8lsNDcRZrUmC2o5kj9MURdhzxSOOwo91yY4pt5oc3+rA7";

                _apiContext.ApiCredential = apiCredential;
                _apiContext.Site = SiteCodeType.US;
            }
        }
    }
}
