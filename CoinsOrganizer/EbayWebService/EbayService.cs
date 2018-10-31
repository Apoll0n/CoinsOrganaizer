using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;

namespace EbayWebService
{
    public static class EbayService
    {
        private static ApiContext _apiContext = null;
        private static Timer _timer = null;

        public static ItemTypeCollection ActiveList { get; set; }
        public static OrderTransactionTypeCollection SoldList { get; set; }

        public static void InitializeEbay()
        {
            GetApiContext();

            if (_timer == null)
            {
                _timer = new Timer(RefreshEbayData, null, 0, TimeSpan.FromMinutes(5).Milliseconds);
            }
        }

        private static void RefreshEbayData(object state)
        {
            //eBayAPIInterfaceService faf = new eBayAPIInterfaceService();
            //faf.SoapRequest
            var apicall = new GetMyeBaySellingCall(_apiContext);

            apicall.ActiveList = new ItemListCustomizationType();
            apicall.ActiveList.Sort = ItemSortTypeCodeType.EndTime;
            apicall.SoldList = new ItemListCustomizationType();
            apicall.SoldList.Sort = ItemSortTypeCodeType.EndTime;

            apicall.ActiveList.Pagination = new PaginationType { EntriesPerPage = 500 };
            apicall.SoldList.Pagination = new PaginationType { EntriesPerPage = 500 };

            apicall.GetMyeBaySelling();

            ActiveList = apicall.ActiveListReturn.ItemArray;
            SoldList = apicall.SoldListReturn.OrderTransactionArray;
        }

        /// <summary>
        /// Populate eBay SDK ApiContext object with data from application configuration file
        /// </summary>
        /// <returns>ApiContext</returns>
        private static void GetApiContext()
        {

            //string accessToken = "AgAAAA**AQAAAA**aAAAAA**QfmvWw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6ADkoehCJKBqAidj6x9nY+seQ**CbUEAA**AAMAAA**g72igHsAWb44hhUnierXvX8o4YyDEuaXgtczBFz1Rh5R6AC0t9QZhukhCiupG1xD8VE3Ae20Zy2qSo61+GyJuRZjZLZ58zGt0b8RSPsvOHuIWjnNuPUq+6g16N8QQexnG2AMwiGx26GH9BO/AbPEjMWt8bZ3GCocnr8ZZeA5kWPFaGpah/3k8kDc3bC4X4FhQ73x8NJp42UsN/fTfOtPJzpHK9qj7r4344svvO1IjPuDktKE/zPb3lSG+5cPJ3rCKkNPmpwxwRE4hS7gUTHp/AvofiYtyx5GTzs0T4X1DMlloSdIA2vFjOBXIKdAGWfl5oku9yeQICg62io7Mvp+jyMWxvhpNwRRPXVyr0n84R9q9G3gNVcLo8gAeCyjNX26/+Jc980h5vbUz5eUoCn9evVMyXbW/GUu09pIEm/WLk5s5NwQlLhOHaLAqhit7lRPW1LE21nNmno+XY5vdOak4HjOBmq7PaUDjzvYuaZKU0Vn0WI/34TxUzTGRZIRzP1OTs2iwEu5CAVSEOBrpgU47yayY1ClhfUhTPVCVtMh95yQY/qvS3k4LjVfKV6PFfIJF6RFU/+gqHfLZlbie5q7Vu0hTO3xe9pcR+IvCAfoZDX+YhIYcwMn3MXzFBzVs++O2yQ8BBQuOZd+LwjEudtWgDIyWUbMrKvu0idVEmJt9PHHZB/oybJjWc7g5OaKbnm1eG2mATa53VA87pdsoIV8lsNDcRZrUmC2o5kj9MURdhzxSOOwo91yY4pt5oc3+rA7";

            //Chilkat.Http http = new Chilkat.Http();

            //string apiCall = "uploadFile";
            //string fileAttachmentUuid = "<urn:uuid:bb47b86a237311e793ae92361f002671>";
            //string xmlUuid = "<urn:uuid:bb47b766237311e793ae92361f002671>";

            //Chilkat.HttpRequest req = new Chilkat.HttpRequest();

            //req.HttpVerb = "POST";
            //req.Path = "/FileTransferService";

            //Chilkat.StringBuilder sbContentType = new Chilkat.StringBuilder();
            //sbContentType.Append("multipart/related; type=\"application/xop+xml\"; start=\"XMLUUID\"; start-info=\"text/xml\"");
            //int replaceCount = sbContentType.Replace("XMLUUID", xmlUuid);
            //req.ContentType = sbContentType.GetAsString();

            //req.AddHeader("X-EBAY-SOA-SERVICE-NAME", "FileTransferService");
            //req.AddHeader("X-EBAY-SOA-OPERATION-NAME", apiCall);
            //req.AddHeader("X-EBAY-SOA-SECURITY-TOKEN", accessToken);
            //req.AddHeader("X-EBAY-SOA-REQUEST-DATA-FORMAT", "XML");
            //req.AddHeader("X-EBAY-SOA-RESPONSE-DATA-FORMAT", "XML");
            //req.AddHeader("User-Agent", "AnythingYouWant");

            //string pathToFileOnDisk1 = "qa_data/ebay/uploadFileRequest.xml";
            //bool success = req.AddFileForUpload("uploadFileRequest.xml", pathToFileOnDisk1);
            //if (success != true)
            //{
            //    Debug.WriteLine(req.LastErrorText);
            //    return;
            //}

            //string pathToFileOnDisk2 = "qa_data/ebay/BulkDataExchangeRequests.gz";
            //success = req.AddFileForUpload("BulkDataExchangeRequests.gz", pathToFileOnDisk2);
            //if (success != true)
            //{
            //    Debug.WriteLine(req.LastErrorText);
            //    return;
            //}

            ////  Add sub-headers for each file in the request.
            //req.AddSubHeader(0, "Content-Type", "application/xop+xml; charset=UTF-8; type=\"text/xml\"");
            //req.AddSubHeader(0, "Content-Transfer-Encoding", "binary");
            //req.AddSubHeader(0, "Content-ID", xmlUuid);
            //req.AddSubHeader(1, "Content-Type", "application/octet-stream");
            //req.AddSubHeader(1, "Content-Transfer-Encoding", "binary");
            //req.AddSubHeader(1, "Content-ID", fileAttachmentUuid);

            //Chilkat.HttpResponse resp = http.SynchronousRequest("storage.sandbox.ebay.com", 443, true, req);
            //if (http.LastMethodSuccess != true)
            //{
            //    Debug.WriteLine(http.LastErrorText);
            //    return;
            //}

            //Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));

            //Chilkat.Xml xml = new Chilkat.Xml();
            //xml.LoadXml(resp.BodyStr);

            //if (resp.StatusCode != 200)
            //{
            //    Debug.WriteLine(xml.GetXml());
            //    Debug.WriteLine("Failed.");

            //    return;
            //}

            //  We still may have a failure.  The XML needs to be checked.
            //  A failed response might look like this:

            //  	<?xml version="1.0" encoding="UTF-8" ?>
            //  	<uploadFileResponse xmlns="http://www.ebay.com/marketplace/services">
            //  	    <ack>Failure</ack>
            //  	    <errorMessage>
            //  	        <error>
            //  	            <errorId>1</errorId>
            //  	            <domain>Marketplace</domain>
            //  	            <severity>Error</severity>
            //  	            <category>Application</category>
            //  	            <message>Task Reference Id is invalid</message>
            //  	            <subdomain>FileTransfer</subdomain>
            //  	        </error>
            //  	    </errorMessage>
            //  	    <version>1.1.0</version>
            //  	    <timestamp>2017-04-18T01:05:27.475Z</timestamp>
            //  	</uploadFileResponse>

            //  A successful response looks like this:

            //  	<?xml version="1.0" encoding="UTF-8" ?>
            //  	<uploadFileResponse xmlns="http://www.ebay.com/marketplace/services">
            //  	    <ack>Success</ack>
            //  	    <version>1.1.0</version>
            //  	    <timestamp>2017-04-18T01:22:47.853Z</timestamp>
            //  	</uploadFileResponse>

            //Debug.WriteLine(xml.GetXml());

            ////  Get the "ack" to see if it's "Failure" or "Success"
            //if (xml.ChildContentMatches("ack", "Success", false))
            //{
            //    Debug.WriteLine("Success.");
            //}
            //else
            //{
            //    Debug.WriteLine("Failure.");
            //}

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
