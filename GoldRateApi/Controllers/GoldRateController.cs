using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;
using GoldRateApi.Models;
using System.Text.RegularExpressions;
using GoldRateApi.CommonClasses;

namespace GoldRateApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoldRateController : Controller
    {
        private readonly ILogger<GoldRateController> _logger;
        private readonly DbGoldRateContext dbGoldRateContext;
       // private readonly String websiteUrl = "https://dubaicityofgold.com/";
        // Constructor
        public GoldRateController(ILogger<GoldRateController> logger,DbGoldRateContext _dbGoldRateContext)
        {
            _logger = logger;
            dbGoldRateContext = _dbGoldRateContext;
        }
        [HttpGet("{Country}")]
        public async Task<JsonResult> Get(string Country,string Currency="")
        {

            Dictionary<string, object> error = new Dictionary<string, object>();
            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            if (Country == null || Country == "")
            {
                error.Add("ErrorCode", 405);
                error.Add("ErrorMessage", "Country Not Found");
                return Json(error);
            }
            if (Country != null && Country != "" && Country.ToUpper() == "UAE")
            {
                return await UpdateUAEGoldRate();
            }
            else
            {
                error.Add("ErrorCode", 405);
                error.Add("ErrorMessage", "Country Not Found");
                return Json(error);
            }

        }

        private async Task<List<dynamic>> GetPageDataGLNW(string url, List<dynamic> results)
        {
            int iCount = 0;
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            // Debug
            //_logger.LogInformation(document.DocumentElement.OuterHtml);

            //  var Remittance = document.QuerySelectorAll("div[id^=gn-widget-remittance]");
            var Gold = document.QuerySelectorAll("div[id^=gn-gold-rate]");
            //var Silver = document.QuerySelectorAll("div[id^=gn-widget-silver]");
            //var Fuel = document.QuerySelectorAll("div[id^=gn-fuel-price]");
            //var Commodity = document.QuerySelectorAll("div[id^=gn-widget-commodity]");



            foreach (var _GoldItem in Gold)
            {
                iCount = 0;
                var _GoldRows = _GoldItem.QuerySelectorAll("tr");
                foreach (var _GoldRow in _GoldRows)
                {
                    mGoldRate _GoldRate = new mGoldRate();
                    if (iCount == 0) { iCount++; continue; }

                    var _Goldth = _GoldRow.QuerySelectorAll("th");
                    var _Goldtd = _GoldRow.QuerySelectorAll("td");

                    _GoldRate.GoldCarat = _Goldth[0].TextContent.Trim();
                    if (_Goldtd.Count() >= 0 && _Goldtd[0].TextContent != null && _Goldtd[0].TextContent.Trim() != "")
                    {
                        _GoldRate.GoldPriceMorning = Convert.ToDouble(_Goldtd[0].TextContent.Trim());
                    }
                    if (_Goldtd.Count() >= 1 && _Goldtd[1].TextContent != null && _Goldtd[1].TextContent.Trim() != "")
                    {
                        _GoldRate.GoldPriceAfternoon = Convert.ToDouble(_Goldtd[1].TextContent.Trim());
                    }
                    if (_Goldtd.Count() >= 2 && _Goldtd[2].TextContent != null && _Goldtd[2].TextContent.Trim() != "")
                    {
                        _GoldRate.GoldPriceEvening = Convert.ToDouble(_Goldtd[2].TextContent.Trim());
                    }
                    if (_Goldtd.Count() >= 3 && _Goldtd[3].TextContent != null && _Goldtd[3].TextContent.Trim() != "")
                    {
                        _GoldRate.GoldPriceYesterday = Convert.ToDouble(_Goldtd[3].TextContent.Trim());
                    }
                    _GoldRate.UpdatedDateTime = DateTime.Now;
                    results.Add(_GoldRate);
                }
            }



            return results;
        }
        private async Task<JsonResult> CheckForUpdatesGLNW(string url, string mailTitle)
        {
            // We create the container for the data we want
            List<dynamic> dyGoldRate = new List<dynamic>();

            /**
             * GetPageData will recursively fill the container with data
             * and the await keyword guarantees that nothing else is done
             * before that operation is complete.
             */
            await GetPageDataGLNW(url, dyGoldRate);

            

            return Json(dyGoldRate.ToList());

            // TODO: Diff the data
        }
        private async Task<List<dynamic>> GetPageData(string url, List<dynamic> results)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            // Debug
            //_logger.LogInformation(document.DocumentElement.OuterHtml);

            var advertrows = document.QuerySelectorAll("table");

            foreach (var row in advertrows)
            {

                var GoldRow = document.QuerySelectorAll("tr");

                int iCount = 0;
                foreach (var item in GoldRow)
                {

                    if (iCount == 0) { iCount++; continue; }
                    // Create a container object
                    mGoldRate _GoldRate = new mGoldRate();

                    string ItemContaint = item.TextContent;
                    List<string> lstValue = new List<string>();
                    lstValue = ItemContaint.Replace('\t', '\n').Split('\n').ToList();

                    foreach (var listValue in lstValue)
                    {
                        if (listValue != null && listValue.Trim() != "")
                        {

                            if (listValue.ToUpper().Contains("K"))
                            {
                                _GoldRate.GoldCarat = listValue.Trim();
                            }
                            if (listValue.ToUpper().Contains("AED"))
                            {
                                _GoldRate.GoldPriceMorning = Convert.ToDouble(listValue.Replace("AED", "").Replace(" ", ""));
                            }
                            _GoldRate.UpdatedDateTime = DateTime.Now;
                        }
                    }
                    if (_GoldRate.GoldCarat != null && _GoldRate.GoldCarat.Trim() != "")
                    {
                        results.Add(_GoldRate);
                    }


                }
            }

            // Check if a next page link is present


            return results;
        }
        private async Task<JsonResult> CheckForUpdates(string url, string mailTitle)
        {
            // We create the container for the data we want
            List<dynamic> dyGoldRate = new List<dynamic>();

            /**
             * GetPageData will recursively fill the container with data
             * and the await keyword guarantees that nothing else is done
             * before that operation is complete.
             */
            await GetPageData(url, dyGoldRate);
            return Json(dyGoldRate.ToList());

            // TODO: Diff the data
        }

        private async Task<JsonResult>  UpdateUAEGoldRate()
        {
            return await CheckForUpdatesGLNW("https://gulfnews.com/gold-forex", "Web-Scraper updates");
        }

    }
}
#region "Commented Code"


//foreach (var _RemittanceItem in Remittance)
//{
//    iCount = 0;
//    var _RemittanceRows = document.QuerySelectorAll("tr");
//    foreach (var _RemittanceRow in _RemittanceRows)
//    {
//        if (iCount == 0) { iCount++; continue; }

//        var _Remittanceth = _RemittanceRow.QuerySelectorAll("th");
//        var _Remittancetd = _RemittanceRow.QuerySelectorAll("td");

//        string ItemContaint = _RemittanceRow.TextContent;
//        List<string> lstValue = new List<string>();
//        lstValue = ItemContaint.Replace('\t', '\n').Split(" ").ToList();


//    }
//}
//}

/*
foreach (var row in advertrows)
{

    var GoldRow = document.QuerySelectorAll("tr");

    int iCount = 0;
    foreach (var item in GoldRow)
    {

        if (iCount == 0) { iCount++; continue; }
        // Create a container object
        mGoldRate _GoldRate = new mGoldRate();

        string ItemContaint = item.TextContent;
        List<string> lstValue = new List<string>();
        lstValue = ItemContaint.Replace('\t', '\n').Split('\n').ToList();

        foreach (var listValue in lstValue)
        {
            if (listValue != null && listValue.Trim() != "")
            {

                if (listValue.ToUpper().Contains("K"))
                {
                    _GoldRate.GoldCarat = listValue.Trim();
                }
                if (listValue.ToUpper().Contains("AED"))
                {
                    _GoldRate.GoldPrice = Convert.ToDecimal(listValue.Replace("AED", "").Replace(" ", ""));
                }
                _GoldRate.UpdatedDateTime = DateTime.Now;
            }
        }
        if (_GoldRate.GoldCarat != null && _GoldRate.GoldCarat.Trim() != "")
        {
            results.Add(_GoldRate);
        }


    }
}

  // This is where the HTTP request happens, returns <IDocument> that // we can query later
            //var document =  context.OpenAsync("https://dubaicityofgold.com/");
            // Log the data to the console
            // _logger.LogInformation(document..OuterHtml);
            // return "Hello";

            //CheckForUpdates("https://dubaicityofgold.com/", "Web-Scraper updates");
            //return await CheckForUpdates("https://dubaicityofgold.com/", "Web-Scraper updates"); 


*/
#endregion