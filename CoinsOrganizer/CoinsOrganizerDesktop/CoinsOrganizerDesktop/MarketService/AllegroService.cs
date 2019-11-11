using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CoinsOrganizerDesktop.AllegroWebApiService;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using Newtonsoft.Json;
using RestSharp;

namespace CoinsOrganizerDesktop.MarketService
{
    public static class TimerEvent
    {
        private static Timer _timer = null;
        public static event EventHandler TimerFired;

        public static void InitializeTimer()
        {
            _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        }

        private static void Callback(object state)
        {
            if (TimerFired != null)
            {
                TimerFired(null, EventArgs.Empty);
            }
        }
    }

    public static class AllegroService
    {
        private static servicePortClient _apiContext = null;
        private static string _login = null;
        private static string _accessToken;
        private static DispatcherTimer _timer = null;
        private static Timer _timer2 = null;
        private static readonly SellItemStruct _emptyItem = new SellItemStruct { itemId = -1};

        public static List<SellItemStruct> ActiveList { get; set; }
        public static List<SoldItemStruct> SoldList { get; set; }

        public static void InitializeAllegro()
        {
            //GetApiContext();
            AllegroRestApiAuthorization();

            if (ActiveList == null)
            {
                ActiveList = new List<SellItemStruct>();
            }

            if (SoldList == null)
            {
                SoldList = new List<SoldItemStruct>();
            }

            if (_timer == null)
            {
                //_timer2 = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
                //_timer = new DispatcherTimer(TimeSpan.FromMinutes(1), DispatcherPriority.Background,
                //    RefreshAllegroData, Dispatcher.CurrentDispatcher);
                //_timer.Start();
                //_timer.

                //CheckDublicates();

                //GetAllegroItems(0,0);

                //CheckDublicates();


                RefreshAllegroData(null, null);

            }
        }

        private static void Mainm()
        {
            string readText = File.ReadAllText(@"code.txt");
            _accessToken = readText;

            var offers = GetOffers();
        }

        private static async void AllegroRestApiAuthorization()
        {
            var fileLines = File.ReadLines(@"code.txt");
            _accessToken = fileLines.First();
            var date = DateTime.Parse(fileLines.Last());

            var hours = (DateTime.Now - date).TotalHours;

            if (hours >= 12)
            {
                string redirectURI = "http://127.0.0.1:1072/";
                string authorizationEndpoint2 = "https://allegro.pl/auth/oauth/authorize";
                string clientId = "dc3029f850b049f6a07ec1454a08faed";
                string secretId = "9kISOUUOEnnlXeE0czWDYIPqzVHZIXPcrY4gPEB2KNWgErsrhu7yZt8mP62aIWy1";

                var http = new HttpListener();
                http.Prefixes.Add(redirectURI);
                http.Start();

                string authorizationRequest = string.Format("{0}?response_type=code&client_id={1}",
                    authorizationEndpoint2, clientId);

                System.Diagnostics.Process.Start(authorizationRequest);
                Thread.Sleep(2000);
                var context = await http.GetContextAsync();

                //this.Activate();

                var response = context.Response;
                response.OutputStream.Close();
                http.Stop();
                var code = context.Request.QueryString.Get("code");
                AllegroConnect(code);
            }
        }


        public static void AllegroConnect(string code)
        {
            string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes("dc3029f850b049f6a07ec1454a08faed:9kISOUUOEnnlXeE0czWDYIPqzVHZIXPcrY4gPEB2KNWgErsrhu7yZt8mP62aIWy1"));

            var client = new RestClient("https://allegro.pl/auth/oauth/token?grant_type=authorization_code&code=" + code);
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", $"Basic {base64}");
            var response = client.Execute(request);

            File.WriteAllText(@"code.txt", response.Content + Environment.NewLine + DateTime.Now);
        }

        public static IList<OfferJson> GetOffers()
        {
            var accessToken = JsonConvert.DeserializeObject<AuthorizationJson>(_accessToken);

            var client = new RestClient("https://api.allegro.pl/");
            List<OfferJson> list = new List<OfferJson>();
            bool loading = true;

            while (loading)
            {
                var request = new RestRequest(" sale/offers?publication.status=ACTIVE&limit=1000", Method.GET);
                var limitOffset = list.Count == 0 ? 0 : 1000;
                request.AddHeader("Authorization", $"Bearer {accessToken.AccessToken}");
                request.AddHeader("Accept", "application/vnd.allegro.beta.v1+json");
                request.AddHeader("Content-Type", "application/vnd.allegro.beta.v1+json");
                request.AddParameter("offset", limitOffset);

                var response = client.Execute(request);
                var deserializeObject = JsonConvert.DeserializeObject<OffersJson>(response.Content);

                if (deserializeObject.Offers.Count < 1000)
                {
                    loading = false;
                }

                list.AddRange(deserializeObject.Offers);
            }

            return list;
        }

        private static void RefreshAllegroData(object sender, EventArgs eventArgs)
        {
            ActiveList.Clear();
            SoldList.Clear();


            var offers = GetOffers();
        }

        public static int GetItemCountById(int index)
        {
            var count = ActiveList.Count(x => x.itemTitle.Contains("*(" + index + ")"));

            if (count > 1)
            {
                var it = ActiveList.Where(x => x.itemTitle.Contains("*(" + index + ")"));
            }
            return count;
        }

        public static SellItemStruct GetItemById(int index)
        {
            var item = ActiveList.FirstOrDefault(x => x.itemTitle.Contains("*(" + index + ")"));

            return item ?? _emptyItem;
        }

        private static void Callback(object state)
        {
            RefreshAllegroDataOld(null, null);
        }

        public static void EditItem()
        {
            var editCopyItem = ActiveList.Single(x => x.itemId == 7776239914);
            AfterSalesServiceConditionsStruct afterSalesServiceConditionsStruct;
            string additionalServicesGroup;

            foreach (var sellItemStruct in ActiveList)
            {
                if (sellItemStruct.itemEndTimeLeft != "Zakończona")
                {

                    var copyItemFields = _apiContext.doGetItemFields(_login, sellItemStruct.itemId,
                        out afterSalesServiceConditionsStruct, out additionalServicesGroup);

                    var titleField = copyItemFields[0];

                    var ind = titleField.fvalueString.LastIndexOf("(", StringComparison.InvariantCulture);
                    if (ind != -1 && titleField.fvalueString.LastIndexOf("*", StringComparison.Ordinal) + 1 != ind)
                    {

                    copyItemFields[0].fvalueString = titleField.fvalueString.Insert(ind, "*");


                    var firstEdit = _apiContext.doChangeItemFields(_login, sellItemStruct.itemId,
                        new FieldsValue[]
                        {
                            copyItemFields[0]

                        }
                        , null, 0, null, null, null, null);
                    }

                }
            }
            //titleField.fvalueString =


            //    var ind = titleField.fvalueString.LastIndexOf("(", StringComparison.InvariantCulture);
            //copyItemFields[0].fvalueString =
            //    titleField.fvalueString.Insert(ind + 1, "i"); //suSubstring(ind).Replace(")", "").Replace("(", "");
            //copyItemFields[0].fvalueString =
            //    titleField.fvalueString.Insert(ind, "*");


            //var firstEdit = _apiContext.doChangeItemFields(_login, 7776239914,
            //    new FieldsValue[]
            //    {
            //        copyItemFields[0]

            //    }
            //    , null, 0, null, null, null, null);
        }
        //var firstEdit = _apiContext.doChangeItemFields(_login, 7659041028,
        //    new FieldsValue[]
        //    {
        //        //fsValu
        //        new FieldsValue{fid = 6, fvalueFloat = 14.99f},
        //        new FieldsValue{fid = 29, fvalueInt = 0},

        //    }
        //    //fieldsList.ToArray()
        //    , new int[] {43, 143, 243 }, 0, null, null, null, null);


        //copyItemFields.;
        //var copyItemFields2 =
        //    _apiContext.doSellSomeAgain()

        //FieldsValue fsValu = copyItemFields.SingleOrDefault(x => x.fid == 6);
        //var fieldsList = new List<FieldsValue>();
        //var ignoreId = new int[] {311, 1, 24, 337, 2, 3, 4, 5, 6, 12, 15, 16, 17, 18, 19, 28, 29, 30, 33, 34, 340, 341,10};
        //foreach (var copyItemField in copyItemFields)
        //{
        //    if (copyItemField.fvalueFloat == 9.4f)
        //    {

        //    }

        //    if (copyItemField.fvalueFloat == 18f)
        //    {

        //    }
        //    if (copyItemField.fvalueFloat != 0f)
        //    {

        //    }
        //    if (!ignoreId.Any(x=>x.Equals(copyItemField.fid)))
        //    {
        //        fieldsList.Add(copyItemField);
        //    }
        //}


        //fsValu.fvalueFloat = 14.99f;
        //var editTargetItem = ActiveList.Single(x => x.itemId == 7655838679);

        //var copyItemFields22 = _apiContext.doGetItemFields(_login, 7776239914, out afterSalesServiceConditionsStruct, out additionalServicesGroup);
        //foreach (var copyItemField in copyItemFields22)
        //{
        //    if (copyItemField.fvalueFloat == 9.4f)
        //    {

        //    }

        //    if (copyItemField.fvalueFloat == 18f)
        //    {

        //    }
        //    if (copyItemField.fvalueFloat != 0f)
        //    {

        //    }
        //}
        //var firstEdit = _apiContext.doChangeItemFields(_login, 7659041028,
        //    new FieldsValue[]
        //    {
        //        //fsValu
        //        new FieldsValue{fid = 6, fvalueFloat = 14.99f},
        //        new FieldsValue{fid = 29, fvalueInt = 0},

        //    }
        //    //fieldsList.ToArray()
        //    , new int[] {43, 143, 243 }, 0, null, null, null, null);


        //SaveAsBinaryFormat(fieldsList, @"SaveTemplates\default_fields.dat");
        //var dfsdf =LoadFromBinaryFile<List<FieldsValue>>(new DirectoryInfo(@"SaveTemplates").FullName + @"\default_fields.dat");

        //foreach (var file in
        //    new DirectoryInfo(@"SaveTemplates")..EnumerateFiles(string.Format(@"{0}.dat", templateSaveAsName),
        //        SearchOption.AllDirectories))
        //{
        //    var loadTemplate = (AspectsTemplateModel)LoadFromBinaryFile<AspectsTemplateModel>(file.FullName);
        //    AspectsTemplates.Add(new AspectsTemplateModel(file.Name.Replace(file.Extension, ""),
        //        loadTemplate.Aspects, loadTemplate.CategoryId, loadTemplate.TemplateName, loadTemplate.Uniquekey));
        //}
        //}

        private static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            var binFormat = new BinaryFormatter();

            if (!Directory.Exists(@"SaveTemplates"))
            {
                Directory.CreateDirectory("SaveTemplates");
            }

            using (Stream fStream = new FileStream(fileName,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
        }

        private static object LoadFromBinaryFile<T>(string fileName)
        {
            var binFormat = new BinaryFormatter();
            T deserialize;
            using (Stream fStream = File.OpenRead(fileName))
            {
                deserialize = (T)binFormat.Deserialize(fStream);
            }
            return deserialize;
        }

        public static void CheckDublicates()
        {
            var hasInd = ActiveList.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            var dontHaveInd = ActiveList
                .Where(x => !x.itemTitle.Contains("(") && !x.itemTitle.Contains(")"))/*.Select(x => x.itemTitle)*/;
            var indexes = hasInd
                //    .Where(x=>
                //{
                //    var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
                //    var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
                //    int result;
                //    return int.TryParse(coinIndex, out result);
                //})
                .SelectMany(x =>
                    {
                        var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
                        var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
                        int result;
                        if (!int.TryParse(coinIndex, out result))
                        {
                            return new[] { new { Index = -1, ItemID= x.itemId, Index2 = coinIndex, x } };
                        }
                        return new[] { new { Index = result, ItemID = x.itemId, Index2 = coinIndex, x } };
                    }

                ).OrderBy(x => x.Index);
            var indexesList = indexes.ToList();

            for (int i = 0; i < indexesList.Count(); i++)
            {
                var ind = indexesList[i].Index;
                var indasasd = indexesList[i];

                if (!_indexesChecked.Contains(ind) && !ind.Equals(-1) && ind <= 3548)
                { }
            }

            var duplicates = indexes
                .GroupBy(i => i.Index)
                .Where(g => g.Count() > 1);



            foreach (var sellItemStruct in ActiveList)
            {
                if (sellItemStruct.itemBiddersCounter>0)
                {
                    
                }
            }
            //.Select(g => g.Key);

        }


        private static void GetAllegroItems(int fromId, int toId)
        {
            var hasInd = ActiveList.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            var dontHaveInd = ActiveList.Where(x => !x.itemTitle.Contains("(") && !x.itemTitle.Contains(")")).ToList();
            var indexes = hasInd
                .SelectMany(x =>
                    {
                        var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
                        var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
                        int result;
                        if (!int.TryParse(coinIndex, out result))
                        {
                            return new[] {new {Index = -1, ItemID = x.itemId, Index2 = coinIndex, x}};
                        }
                        return new[] {new {Index = result, ItemID = x.itemId, Index2 = coinIndex, x}};
                    }

                ).OrderBy(x => x.Index).ToList();

            var def = (List<FieldsValue>) LoadFromBinaryFile<List<FieldsValue>>(
                new DirectoryInfo(@"SaveTemplates").FullName + @"\default_fields.dat");

            for (int i = 0; i < dontHaveInd.Count(); i++)
            {
                var list = dontHaveInd[i];

                var firstEdit = _apiContext.doChangeItemFields(_login, list.itemId,
                    def.ToArray()
                    , new int[] {43, 143, 243}, 0, null, null, null, null);
            }
        }




        private static void RefreshAllegroDataOld(object sender, EventArgs eventArgs)
        {
            ActiveList.Clear();
            SellItemStruct[] sellItems;
            SoldItemStruct[] soldItems;

            var itemsCount = _apiContext.doGetMySellItems(_login, null, null, null, 0, null, 0, 0, out sellItems);
            for (int i = 1; i < Math.Ceiling(itemsCount / 100d) + 1; i++)
            {
                foreach (var sellItem in sellItems)
                {
                    ActiveList.Add(sellItem);
                }

                _apiContext.doGetMySellItems(_login, null, null, null, 0, null, 0, i, out sellItems);
            }

            var itemsCount2 = _apiContext.doGetMySoldItems(_login, null, null, null, 0, null, 0, 0, out soldItems);
            for (int i = 1; i < Math.Ceiling(itemsCount2 / 100d) + 1; i++)
            {
                foreach (var soldItem in soldItems)
                {
                    SoldList.Add(soldItem);
                }

                _apiContext.doGetMySoldItems(_login, null, null, null, 0, null, 0, i, out soldItems);
            }

            
        }

        private static void GetApiContext()
        {
            if (_apiContext == null)
            {
                var webApiKey = "640314ba49b245f7be0a4fce0b76a6e0";

                var siteId = 1;
                var siteVersion = 1416396021;

                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                {
                    MaxReceivedMessageSize = 15000000
                };

                _apiContext =
                    new servicePortClient(binding, new EndpointAddress("https://webapi.allegro.pl/service.php"));
                long userId, serverTime;
                var syst = _apiContext.doQueryAllSysStatus(1, webApiKey);
                _login = _apiContext.doLogin("mosze888", "KGZkRsY5ottgDuW", 1, webApiKey, syst[0].verKey, out userId,
                    out serverTime);
            }
        }

        private static int[] _indexesChecked = new int[]
        {
            249,
            401,
            411,
            456,
            457,
            458,
            459,
            463,
            466,
            480,
            482,
            483,
            485,
            487,
            493,
            502,
            514,
            550,
            551,
            563,
            596,
            605,
            612,
            613,
            633,
            634,
            647,
            650,
            684,
            715,
            730,
            777,
            778,
            831,
            835,
            846,
            866,
            867,
            872,
            878,
            893,
            929,
            951,
            953,
            960,
            962,
            981,
            990,
            992,
            994,
            1001,
            1002,
            1003,
            1004,
            1005,
            1006,
            1007,
            1008,
            1010,
            1011,
            1021,
            1029,
            1030,
            1032,
            1034,
            1035,
            1039,
            1040,
            1041,
            1042,
            1047,
            1050,
            1085,
            1105,
            1119,
            1121,
            1125,
            1133,
            1141,
            1142,
            1157,
            1161,
            1163,
            1165,
            1167,
            1177,
            1180,
            1181,
            1210,
            1211,
            1212,
            1214,
            1215,
            1216,
            1217,
            1218,
            1220,
            1224,
            1225,
            1228,
            1232,
            1240,
            1241,
            1242,
            1243,
            1245,
            1251,
            1252,
            1257,
            1262,
            1268,
            1269,
            1270,
            1271,
            1274,
            1275,
            1278,
            1281,
            1282,
            1283,
            1284,
            1285,
            1286,
            1287,
            1288,
            1291,
            1293,
            1295,
            1296,
            1299,
            1300,
            1301,
            1302,
            1303,
            1304,
            1305,
            1306,
            1307,
            1308,
            1309,
            1310,
            1312,
            1313,
            1314,
            1315,
            1316,
            1317,
            1318,
            1319,
            1321,
            1322,
            1323,
            1324,
            1325,
            1329,
            1331,
            1335,
            1336,
            1337,
            1340,
            1343,
            1344,
            1346,
            1347,
            1348,
            1350,
            1351,
            1352,
            1353,
            1355,
            1356,
            1357,
            1359,
            1360,
            1361,
            1366,
            1371,
            1376,
            1377,
            1382,
            1390,
            1400,
            1403,
            1404,
            1405,
            1407,
            1409,
            1410,
            1411,
            1413,
            1415,
            1416,
            1417,
            1418,
            1419,
            1420,
            1421,
            1422,
            1423,
            1424,
            1425,
            1426,
            1427,
            1428,
            1429,
            1430,
            1431,
            1432,
            1433,
            1434,
            1435,
            1436,
            1437,
            1438,
            1439,
            1440,
            1444,
            1446,
            1447,
            1448,
            1450,
            1465,
            1466,
            1467,
            1468,
            1469,
            1470,
            1471,
            1472,
            1473,
            1474,
            1475,
            1476,
            1478,
            1479,
            1480,
            1481,
            1483,
            1485,
            1486,
            1488,
            1490,
            1491,
            1492,
            1494,
            1496,
            1498,
            1499,
            1500,
            1501,
            1502,
            1503,
            1504,
            1505,
            1507,
            1508,
            1509,
            1510,
            1511,
            1512,
            1513,
            1515,
            1517,
            1518,
            1519,
            1522,
            1524,
            3000,
            3001,
            3002,
            3003,
            3004,
            3005,
            3006,
            3007,
            3008,
            3009,
            3010,
            3011,
            3012,
            3013,
            3014,
            3015,
            3016,
            3017,
            3018,
            3019,
            3021,
            3023,
            3024,
            3025,
            3026,
            3027,
            3028,
            3029,
            3030,
            3031,
            3032,
            3033,
            3034,
            3035,
            3036,
            3037,
            3038,
            3039,
            3040,
            3041,
            3042,
            3043,
            3044,
            3045,
            3046,
            3047,
            3048,
            3053,
            3059,
            3060,
            3062,
            3063,
            3066,
            3068,
            3069,
            3075,
            3076,
            3077,
            3078,
            3079,
            3082,
            3089,
            3096,
            3097,
            3098,
            3099,
            3101,
            3102,
            3103,
            3104,
            3106,
            3108,
            3110,
            3112,
            3117,
            3119,
            3120,
            3121,
            3122,
            3123,
            3124,
            3125,
            3126,
            3127,
            3128,
            3132,
            3135,
            3137,
            3138,
            3139,
            3140,
            3141,
            3142,
            3143,
            3145,
            3146,
            3147,
            3148,
            3149,
            3150,
            3151,
            3152,
            3153,
            3154,
            3156,
            3157,
            3158,
            3159,
            3160,
            3163,
            3164,
            3165,
            3166,
            3167,
            3168,
            3173,
            3174,
            3175,
            3178,
            3179,
            3180,
            3181,
            3183,
            3184,
            3187,
            3190,
            3192,
            3194,
            3195,
            3196,
            3197,
            3198,
            3199,
            3200,
            3202,
            3204,
            3205,
            3207,
            3208,
            3210,
            3211,
            3212,
            3214,
            3215,
            3216,
            3217,
            3218,
            3221,
            3222,
            3223,
            3224,
            3225,
            3226,
            3227,
            3228,
            3229,
            3231,
            3232,
            3233,
            3234,
            3235,
            3236,
            3237,
            3239,
            3240,
            3241,
            3242,
            3243,
            3244,
            3245,
            3246,
            3247,
            3250,
            3251,
            3253,
            3254,
            3255,
            3256,
            3257,
            3258,
            3259,
            3262,
            3265,
            3268,
            3269,
            3270,
            3272,
            3273,
            3279,
            3283,
            3286,
            3288,
            3305,
            3306,
            3307,
            3308,
            3309,
            3312,
            3313,
            3314,
            3315,
            3316,
            3320,
            3322,
            3323,
            3324,
            3325,
            3326,
            3328,
            3329,
            3330,
            3331,
            3332,
            3334,
            3338,
            3340,
            3341,
            3343,
            3345,
            3346,
            3348,
            3349,
            3350,
            3351,
            3354,
            3355,
            3356,
            3358,
            3359,
            3361,
            3366,
            3369,
            3370,
            3371,
            3374,
            3375,
            3377,
            3378,
            3379,
            3390,
            3395,
            3396,
            3397,
            3399,
            3403,
            3404,
            3405,
            3407,
            3410,
            3411,
            3414,
            3415,
            3416,
            3417,
            3418,
            3419,
            3420,
            3421,
            3423,
            3424,
            3425,
            3427,
            3429,
            3431,
            3432,
            3433,
            3434,
            3435,
            3437,
            3438,
            3439,
            3440,
            3441,
            3442,
            3443,
            3444,
            3445,
            3446,
            3447,
            3448,
            3449,
            3450,
            3452,
            3454,
            3455,
            3456,
            3457,
            3458,
            3459,
            3460,
            3461,
            3463,
            3464,
            3466,
            3467,
            3468,
            3469,
            3470,
            3471,
            3472,
            3475,
            3476,
            3478,
            3479,
            3480,
            3482,
            3483,
            3485,
            3486,
            3487,
            3488,
            3489,
            3490,
            3491,
            3492,
            3493,
            3494,
            3495,
            3496,
            3497,
            3498,
            3499,
            3500,
            3501,
            3502,
            3503,
            3504,
            3505,
            3506,
            3507,
            3508,
            3509,
            3510,
            3511,
            3512,
            3514,
            3518,
            3521,
            3522,
            3523,
            3524,
            3525,
            3527,
            3528,
            3533,
            3534,
            3535,
            3544,
            3545,
            3546,
            3547,
            3548,
            414,
            416,
            417,
            418,
            419,
            420,
            421,
            422,
            423,
            424,
            425,
            427,
            429,
            580,
            913,
            1019,
            822,
            1264,
            1477
        };

    }

}