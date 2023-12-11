using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using prjDB_GamingForm_Show.Models.Entities;
using System;
using System.Net.Http;
using System.Runtime.Caching;
using System.Net.Http.Headers;

namespace prjDB_GamingForm_Show.Controllers
{
    public class EcpayController : Controller
    {
        DbGamingFormTestContext db = new DbGamingFormTestContext();
        //step4 : 新增訂單
        [HttpPost]
        [Route("api/Ecpay/AddOrders")]
        public string AddOrders(Models.Shop.get_localStorage  json)
        {
            EcpayOrder Orders = new EcpayOrder();
            Orders.MemberId = json.MerchantID;
            Orders.MerchantTradeNo = json.MerchantTradeNo;
            Orders.RtnCode = 0; //未付款
            Orders.RtnMsg = "訂單成功尚未付款";
            Orders.TradeNo = json.MerchantID.ToString();
            Orders.TradeAmt = json.TotalAmount;
            Orders.PaymentDate = Convert.ToDateTime(json.MerchantTradeDate);
            Orders.PaymentType = json.PaymentType;
            Orders.PaymentTypeChargeFee = "0";
            Orders.TradeDate = json.MerchantTradeDate;
            Orders.SimulatePaid = 0;
            db.EcpayOrders.Add(Orders);
            db.SaveChanges();
            return "OK";
        }
        // HomeController->Index->ReturnURL所設定的
        [HttpPost]
        [Route("api/Ecpay/AddPayInfo")]
        public HttpResponseMessage AddPayInfo(JObject info)
        {
            try
            {
                var cache = MemoryCache.Default;
                cache.Set(info.Value<string>("MerchantTradeNo"), info, DateTime.Now.AddMinutes(60));
                return ResponseOK();
            }
            catch (Exception e)
            {
                return ResponseError();
            }
        }
        // HomeController->Index->PaymentInfoURL所設定的
        [HttpPost]
        [Route("api/Ecpay/AddAccountInfo")]
        public HttpResponseMessage AddAccountInfo(JObject info)
        {
            try
            {
                var cache = MemoryCache.Default;
                cache.Set(info.Value<string>("MerchantTradeNo"), info, DateTime.Now.AddMinutes(60));
                return ResponseOK();
            }
            catch (Exception e)
            {
                return ResponseError();
            }
        }
        private HttpResponseMessage ResponseError()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("0|Error");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
        private HttpResponseMessage ResponseOK()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("1|OK");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
